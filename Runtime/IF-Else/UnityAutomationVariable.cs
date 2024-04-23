using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;
using BitByte;

#region AllCondition If-Else
[System.Serializable]
public class AllCondition{
    // public bool isVariableSet = false;
    // && || ( )
    public MultipleCondition AndOR;
    public UnityEngine.Object unityObj;
    [FieldList("unityObj", addField:true, addProperty:true, addFunction:true, addValueReturnType:true, disableName = true)]
    public string selectField;
    public Parameters[] parameters1;
    // is ==, >=, <=, != 
    public Operator @operator;
    public ValueOrGM compareWith;
    public UnityEngine.Object unityObj2;
    [FieldList("unityObj2", addField:true, addProperty:true, addFunction:true, addValueReturnType:true, disableName = true)]
    public string selectField2;
    public Parameters[] parameters2;
    [DrawValue("unityObj", "selectField")]
    public SelectedValue valueCompare;

    // For storage
    //----------First Variable-------
    public FieldInfo selectedField1;
    public PropertyInfo selectedProperty1;
    public MethodInfo selectedMethodInfo1;
    public Type[] methodParamTypes1;
    public Type returnType1;

    //------- Second Variable-----------
    public PropertyInfo selectedProperty2;
    public FieldInfo selectedField2;
    public MethodInfo selectedMethodInfo2;
    public Type[] methodParamTypes2;
    public Type returnType2;
}
#endregion
//----------------------------All Enum----------------------------------

public enum MultipleCondition
{
    NULL,
    AND,
    OR,
    OpenBracket,
    CloseBracket,
    ANDOpenBracket,
    OROpenBracket,
    CloseBracketAND,
    CloseBracketOR,
    CloseBracketANDOpenBracket,
    CloseBracketOROpenBracket
}
public enum Operator
{
    EqualTo,
    NotEqualTo,
    GreaterThan,
    GreaterThanEqualTo,
    LessThan,
    LessThanEqualTo,
}
