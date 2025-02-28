using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Muchwood.Utils;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute)), CanEditMultipleObjects]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
