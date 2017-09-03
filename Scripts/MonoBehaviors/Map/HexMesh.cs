using System.Collections.Generic;
using Assets.TurnBasedStrategy.Scripts.Common;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    /// <summary>
    /// The mesh representing a grid of hexagonal cells.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        private Mesh _hexMesh;

        private List<Vector3> _vertices;

        private List<int> _triangles;
        private MeshCollider _meshCollider;
        private List<Color> _colors;

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
        /// Triangulates all the cells in the mesh.
        /// </summary>
        /// <param name="cells">An array of cells to triangulate</param>
        public void Triangulate(HexCell[] cells)
        {
            ClearLists();
            foreach (HexCell t in cells)
            {
                Triangulate(t);
            }
            FinalizeData();
        }

        /// <summary>
        /// Creates the triangles responsible for the hexagon of this cell.
        /// </summary>
        /// <param name="cell">The cell to Triangulate</param>
        public void Triangulate(HexCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for (int i = 0; i < HexCell.Corners.Length - 1; i++)
            {
                AddTriangle(
                    center,
                    center + HexCell.Corners[i],
                    center + HexCell.Corners[i + 1]
                );
                AddTriangleColor(cell.Color);
            }

        }

        private void AddTriangleColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);

        }

        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = _vertices.Count;
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
