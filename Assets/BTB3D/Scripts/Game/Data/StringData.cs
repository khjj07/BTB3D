using BTB3D.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StringData : Data
{

    public static Data Create(string name, string value)
    {
        var instance = CreateInstance<StringData>();
        EditorUtility.SetDirty(instance);
        instance.name = name;
        instance.value = value;
        return instance;
    }
    public string value;

    public override object GetValue()
    {
        return (object)value;
    }
}
