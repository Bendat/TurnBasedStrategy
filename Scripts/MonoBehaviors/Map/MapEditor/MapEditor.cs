using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map.MapEditor
{
    /// <inheritdoc />
    /// <summary>
    /// An editor to change qualities of cells on the map.
    /// </summary>
    public class MapEditor : MonoBehaviour
    {
        /// <summary>
        /// The color pallete available to texture cells.
        /// </summary>
        public Color[] Colors;

        /// <summary>
        /// The MapGrid object responsible for the cells being edited.
        /// </summary>
        public MapGrid MapGrid;

        private Color _activeColor;

        void Awake()
        {
            SelectColor(0);
        }

        void Update()
        {
            if (
                Input.GetMouseButton(0) &&
                !EventSystem.current.IsPointerOverGameObject()
            )
            {
                HandleInput();
            }
        }

        /// <summary>
        /// Sets the active color to assign to a cell.
        /// </summary>
        /// <param name="index"></param>
        public void SelectColor(int index)
        {
            _activeColor = Colors[index];
        }

        private void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                MapGrid.ColorCell(hit.point, _activeColor);
            }
        }

    }
}
