using System.Security.Cryptography.X509Certificates;
using Assets.TurnBasedStrategy.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    /// <inheritdoc />
    /// <summary>
    /// Controller class for creating the map.
    /// </summary>
    public class MapGrid : MonoBehaviour
    {
        /// <summary>
        /// The number of tiles wide the map will be.
        /// </summary>
        public int Width = 6;

        /// <summary>
        /// The number of tiles high the map will be.
        /// </summary>
        public int Height = 6;

        /// <summary>
        /// The HexCell prefab to be used for generating the map.
        /// </summary>
        public HexCell CellPrefab;
        
        /// <summary>
        /// The UI textlabel to display coordinates on.
        /// </summary>
        public Text CellLabelPrefab;

        /// <summary>
        /// The default color for the tiles.
        /// </summary>
        public Color DefaultColor = Color.white;

        private HexCell[] _cells;
        private Canvas _canvas;
        private HexMesh _hexMesh;
        
        private void Awake()
        {
            _cells = new HexCell[Height * Width];
            _canvas = GetComponentInChildren<Canvas>();
            _hexMesh = GetComponentInChildren<HexMesh>();
            CreateCells();
        }

        // Use this for initialization
        private void Start()
        {
            _hexMesh.Triangulate(_cells);
        }

        // Update is called once per frame
        private void Update()
        {
        }
        
        /// <summary>
        /// Sets the material of this cell to a specified color.
        /// </summary>
        /// <param name="position">The world space coordinates under which a HexCell resides..</param>
        /// <param name="color">The color to set the tile</param>
        public void ColorCell(Vector3 position, Color color)
        {
            position = transform.InverseTransformPoint(position);
            MapCoordinates coordinates = MapCoordinates.FromPosition(position);
            int index = coordinates.X + coordinates.Z * Width + coordinates.Z / 2;
            HexCell cell = _cells[index];
            cell.Color = color;
            _hexMesh.Triangulate(_cells);
        }

        private void CreateCells()
        {
            for(int z = 0, i = 0; z < Height; z++)
            {
                for(int x = 0; x < Width; x++)
                {
                    CreateCell(new MapCoordinates(x, z), i++);
                }
            }
        }

        private void CreateCell(MapCoordinates coords, int index)
        {
            var position = new Vector3()
            {
                x = (coords.X + coords.Z * 0.5f - coords.Z / 2) * (HexCell.InnerRadius * 2f),//offsets every second row.
                y = 0f,
                z = coords.Z * (HexCell.OuterRadius * 1.5f)
            };

            HexCell cell = _cells[index] = Instantiate<HexCell>(CellPrefab);
            cell.transform.SetParent(_hexMesh.transform, false);
            cell.transform.localPosition = position;
            cell.Coordinates = MapCoordinates.FromOffsetCoordinates(coords);
            cell.name = $"Cell{cell.Coordinates}";
            cell.Color = DefaultColor;

            var label = Instantiate<Text>(CellLabelPrefab);
            label.rectTransform.SetParent(_canvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text =  cell.Coordinates.ToFlatString();
        }
    }
}
