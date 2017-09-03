using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map.MapEditor
{
    public class TileColorEditor: IEditorComponent
    {
        public Color[] Colors { get; private set; }
        public MapGrid MapGrid { get; private set; }
        
        private Color _activeColor;

        public TileColorEditor(MapGrid grid, Color[] colors)
        {
            MapGrid = grid;
            Colors = colors;
            SelectColor(0);
        }

//        public void Awake()
//        {
//            throw new System.NotImplementedException();
//        }

        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                HandleInput();
            }
        }

        void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                MapGrid.ColorCell(hit.point, _activeColor);
            }
        }

        public void SelectColor(int index)
        {
            _activeColor = Colors[index];
        }
    }
}