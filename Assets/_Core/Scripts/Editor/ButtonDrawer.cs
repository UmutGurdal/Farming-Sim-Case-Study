using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnityEngine.Object), true), CanEditMultipleObjects]
public class ButtonDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (targets.Length == 0)
            return;

        Type commonType = targets[0].GetType();
        MethodInfo[] methods = commonType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods)
        {
            ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttribute == null)
                continue;

            string buttonLabel = string.IsNullOrEmpty(buttonAttribute.Label) ? method.Name : buttonAttribute.Label;

            ParameterInfo[] parameters = method.GetParameters();
            object[] methodParameters = new object[parameters.Length];

            // Initialize method parameters with default values
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo param = parameters[i];

                if (param.ParameterType == typeof(int))
                    methodParameters[i] = EditorGUILayout.IntField(param.Name, 0);
                else if (param.ParameterType == typeof(float))
                    methodParameters[i] = EditorGUILayout.FloatField(param.Name, 0f);
                else if (param.ParameterType == typeof(string))
                    methodParameters[i] = EditorGUILayout.TextField(param.Name, string.Empty);
                else if (param.ParameterType == typeof(bool))
                    methodParameters[i] = EditorGUILayout.Toggle(param.Name, false);
                else
                {
                    EditorGUILayout.LabelField($"Unsupported parameter type: {param.ParameterType.Name}");
                    methodParameters[i] = null;
                }
            }

            GUILayout.Space(10);

            if (GUILayout.Button(buttonLabel))
            {
                foreach (UnityEngine.Object targetObject in targets)
                {
                    Undo.RecordObject(targetObject, $"Invoke {method.Name}");
                    method.Invoke(targetObject, methodParameters);
                    EditorUtility.SetDirty(targetObject);
                }
            }
        }
    }
}
