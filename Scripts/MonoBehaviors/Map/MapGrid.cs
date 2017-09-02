using System.Security.Cryptography.X509Certificates;
using Assets.TurnBasedStrategy.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    public class MapGrid : MonoBehaviour
    {
        public int Width = 6;
        public int Height = 6;

        public HexCell CellPrefab;
        public Text CellLabelPrefab;

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

        private void CreateCells()
        {
            for(int z = 0, i = 0; z < Height; z++)
            {
                for(int x = 0; x < Width; x++)
                {
                    CreateCell(new MapCoordinate(x, z), i++);
                }
            }
        }

        private void CreateCell(MapCoordinate coords, int index)
        {
            var position = new Vector3()
            {
                x = (coords.x + coords.z * 0.5f - coords.z / 2) * (HexCell.InnerRadius * 2f),
                y = 0f,
                z = coords.z * (HexCell.OuterRadius * 1.5f)
            };

            HexCell cell = _cells[index] = Instantiate<HexCell>(CellPrefab);
            cell.transform.SetParent(transform, false);
            cell.name = $"Cell[{coords.x}, {coords.z}]";
            cell.transform.localPosition = position;

            var label = Instantiate<Text>(CellLabelPrefab);
            label.rectTransform.SetParent(_canvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = $"{coords.x}, {coords.z} ";
        }
    }
}
