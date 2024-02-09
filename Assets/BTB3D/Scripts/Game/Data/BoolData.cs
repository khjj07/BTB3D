using System;
using UnityEditor;

namespace BTB3D.Scripts.Data
{
    [Serializable]
    public class BoolData : Data
    {
        public static Data Create(string name, bool value)
        {
            var instance = CreateInstance<BoolData>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            return instance;
        }
        public bool value;

        public override object GetValue()
        {
            return (object)value;
        }
    }
}