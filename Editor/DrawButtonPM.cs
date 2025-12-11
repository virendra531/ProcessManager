using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class DrawButtonPMAttribute: PropertyAttribute
{
    public string buttonName;
    public bool playModeOnly;
    public bool editorModeOnly;
    public DrawButtonPMAttribute(string buttonName = null){
       this.buttonName = buttonName;
   }
}
#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourCustomEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        var type = target.GetType();
        // Iterate over each private or public instance method (no static methods atm)
        foreach (var method in type.GetMethods(BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Instance))
        {
            if (method.GetParameters().Length == 0)
            {
                if(method.GetCustomAttributes(typeof(DrawButtonPMAttribute), true).Length > 0)
                {
                    DrawButtonPMAttribute buttonAttribute = (DrawButtonPMAttribute)method.GetCustomAttributes(typeof(DrawButtonPMAttribute), true)[0];
                    string buttonText = string.IsNullOrEmpty(buttonAttribute.buttonName) ? method.Name : buttonAttribute.buttonName;
                    using (new EditorGUI.DisabledScope( (buttonAttribute.playModeOnly) ? !UnityEditor.EditorApplication.isPlaying : (buttonAttribute.editorModeOnly) ?  UnityEditor.EditorApplication.isPlaying : false))
                    {
                        if (GUILayout.Button(buttonText))
                        {
                            method.Invoke(target, null);
                        }
                    }
                }
            }
            else
            {
                if(method.GetCustomAttributes(typeof(DrawButtonPMAttribute), true).Length > 0)
                {
                    string warning = typeof(DrawButtonPMAttribute).Name + " works only on methods with no parameters";
                    EditorGUILayout.HelpBox(warning, MessageType.Warning);
                }
            }
        }
        DrawDefaultInspector();
    }
}
#endif