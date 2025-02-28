using UnityEditor;
using UnityEngine;

namespace Muchwood.Utils
{
    [CustomPropertyDrawer(typeof(SpritePreviewAttribute), true), CanEditMultipleObjects]
    public class SpritePreviewDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw the sprite field
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property, label);

            // Draw the sprite preview if it's assigned
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue is Sprite sprite)
            {
                Texture2D texture = AssetPreview.GetAssetPreview(sprite);
                if (texture != null)
                {
                    // Draw sprite preview below the field
                    Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, 64, 64);
                    GUI.DrawTexture(previewRect, texture, ScaleMode.ScaleToFit);
                }
            }

            EditorGUI.EndProperty();

        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = EditorGUIUtility.singleLineHeight;
            return (property.objectReferenceValue is Sprite) ? baseHeight + 68 : baseHeight; // Extra space for preview
        }
    }
}

