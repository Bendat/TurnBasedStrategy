using UnityEditor;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Utils
{
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

    /// <inheritdoc />
    /// <summary>
    /// Property drawer class to make fields marked with the ReadOnly attribute visible but immutable within the editor.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}