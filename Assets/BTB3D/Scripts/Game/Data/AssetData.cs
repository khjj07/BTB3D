using BTB3D.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetData : Data
{
    public static Data Create(string name, ScriptableObject value)
    {
        var instance = CreateInstance<AssetData>();
        EditorUtility.SetDirty(instance);
        instance.name = name;
        instance.value = value;
        return instance;
    }

    public ScriptableObject value;

    public override object GetValue()
    {
        return (object)value;
    }
}
