using System.Collections.Generic;
using Assets.TurnBasedStrategy.Scripts.Enums;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    /// <summary>
    ///     The mesh representing a grid of hexagonal cells.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        [SerializeField] private List<Color> _colors;
        private Mesh _hexMesh;
        private MeshCollider _meshCollider;

        private List<int> _triangles;

        private List<Vector3> _vertices;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();
            _meshCollider = gameObject.AddComponent<MeshCollider>();
            _hexMesh.name = "Hex Mesh";
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _colors = new List<Color>();
        }

        /// <summary>
        ///     Triangulates all the cells in the mesh.
        /// </summary>
        /// <param name="cells">An array of cells to triangulate</param>
        public void Triangulate(HexCell[] cells)
        {
            ClearLists();
            foreach (var t in cells)
                Triangulate(t);
            FinalizeData();
        }

        /// <summary>
        ///     Creates the triangles responsible for the hexagon of this cell.
        /// </summary>
        /// <param name="cell">The cell to Triangulate</param>
        private void Triangulate(HexCell cell)
        {
            for (var d = HexDirection.NE; d <= HexDirection.NW; d++)
                Triangulate(d, cell);
        }

        private void Triangulate(HexDirection direction, HexCell cell)
        {
            var center = cell.transform.localPosition;
            var v1 = center + HexCell.GetFirstSolidCorner(direction);
            var v2 = center + HexCell.GetSecondSolidCorner(direction);
 


            Vector3 bridge = HexCell.GetBridge(direction);
            Vector3 v3 = v1 + bridge;
            Vector3 v4 = v2 + bridge;

            AddQuad(v1, v2, v3, v4);

            var prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
            var neighbor = cell.GetNeighbor(direction) ?? cell;
            var nextNeighbor = cell.GetNeighbor(direction.Next()) ?? cell;
            var bridgeColor = (cell.Color + neighbor.Color) * 0.5f;

            AddQuadColor(cell.Color, bridgeColor);
            AddTriangle(v1, center + HexCell.GetFirstCorner(direction), v3);
            AddTriangleColor(
                cell.Color,
                (cell.Color + prevNeighbor.Color + neighbor.Color) / 3f,
                bridgeColor
            );
        }

        private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            var vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _vertices.Add(v4);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 3);
        }

        private void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
        {
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
            _colors.Add(c4);
        }

        private void AddQuadColor(Color c1, Color c2)
        {
            _colors.Add(c1);
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c2);
        }

        private void AddTriangleColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }

        private void AddTriangleColor(Color c1, Color c2, Color c3)
        {
            _colors.Add(c1);
            _colors.Add(c2);
            _colors.Add(c3);
        }

        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }

        private void ClearLists()
        {
            _hexMesh.Clear();
            _vertices.Clear();
            _triangles.Clear();
            _colors.Clear();
        }

        private void FinalizeData()
        {
            _hexMesh.vertices = _vertices.ToArray();
            _hexMesh.triangles = _triangles.ToArray();
            _hexMesh.RecalculateNormals();
            _hexMesh.colors = _colors.ToArray();
            _meshCollider.sharedMesh = _hexMesh;
        }
    }
}