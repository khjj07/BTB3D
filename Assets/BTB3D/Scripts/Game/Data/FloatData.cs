using System;
using UnityEditor;

namespace BTB3D.Scripts.Data
{
    [Serializable]
    public class FloatData : Data
    {

        public override object GetValue()
        {
            return (object)value;
        }
        public static Data Create(string name, float value)
        {
            var instance = CreateInstance<FloatData>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            return instance;
        }
        public float value;
    }
}