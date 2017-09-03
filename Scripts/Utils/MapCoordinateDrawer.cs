using Assets.TurnBasedStrategy.Scripts.Common;
using UnityEditor;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Utils
{
    /// <summary>
    /// Property drawer to display the properties of the MapCoordinates class in an immutable way in the editor.
    /// </summary>
    [CustomPropertyDrawer(typeof(MapCoordinates))]
    public class MapCoordinateDrawer: PropertyDrawer
    {
        public override void OnGUI(
            Rect position, SerializedProperty property, GUIContent label
        )
        {
            MapCoordinates coordinates = new MapCoordinates(
                property.FindPropertyRelative("x").intValue,
                property.FindPropertyRelative("z").intValue
            );
            position = EditorGUI.PrefixLabel(position, label);
            GUI.Label(position, coordinates.ToString());
        }
    }
}
