using DarkPixelRPGUI.Scripts.UI;
using UnityEditor;
using UnityEngine;

namespace DarkPixelRPGUI.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(NullableSerializableField), true)]
    public class NullableSerializablePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var labelPosition = new Rect(5f * 4, position.y, position.width / 2 - 5f * 4, position.height);
            var buttonPosition = new Rect(position.width / 2, position.y, position.width / 2, 20);
            EditorGUI.LabelField(labelPosition, new GUIContent("Nullable " + property.displayName));
            var hasValueProperty = property.FindPropertyRelative("hasValue");
            var valueProperty = property.FindPropertyRelative("value");

            if (hasValueProperty.boolValue)
            {
                if (GUI.Button(buttonPosition, "Set NULL"))
                {
                    hasValueProperty.boolValue = false;
                }

                var propertyPosition = new Rect(buttonPosition.x, buttonPosition.y + 20, buttonPosition.width, buttonPosition.height);
                EditorGUI.PropertyField(propertyPosition, valueProperty, true);
            }
            else
            {
                if (GUI.Button(buttonPosition, "NULL. Click to set"))
                {
                    hasValueProperty.boolValue = true;
                }
            }
            
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            var propertyHeightIndent = 20f;
            if (property.FindPropertyRelative("hasValue").boolValue)
            {
                propertyHeightIndent +=
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"), GUIContent.none) + 1f;
            }

            return propertyHeightIndent;
        }
    }
}