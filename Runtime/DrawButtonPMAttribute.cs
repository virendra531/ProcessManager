using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class DrawButtonPMAttribute : PropertyAttribute
{
    public string buttonName;
    public bool playModeOnly;
    public bool editorModeOnly;
    public DrawButtonPMAttribute(string buttonName = null)
    {
        this.buttonName = buttonName;
    }
}