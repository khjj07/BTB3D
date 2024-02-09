using System;
using UnityEditor;

namespace BTB3D.Scripts.Data
{
    [Serializable]
    public class IntegerData : Data
    {
        public static Data Create(string name, int value)
        {
            var instance = CreateInstance<IntegerData>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            return instance;
        }
        public int value;

        public override object GetValue()
        {
            return (object)value;
        }
    }
}
