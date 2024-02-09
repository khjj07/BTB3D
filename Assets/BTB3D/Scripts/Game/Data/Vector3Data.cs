using System;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Data
{
    [Serializable]
    public class Vector3Data : Data
    {
        public static Data Create(string name, Vector3 value)
        {
            var instance = CreateInstance<Vector3Data>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            return instance;
        }
        public Vector3 value;

        public override object GetValue()
        {
            return (object)value;
        }
    }
}
