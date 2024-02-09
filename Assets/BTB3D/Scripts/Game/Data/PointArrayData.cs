using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Data
{
    public class PointArrayData : Data
    {
        public static Data Create(string name, Vector3Data[] value)
        {
            var instance = CreateInstance<PointArrayData>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            foreach (var child in value)
            {
                
            }
            return instance;
        }
        public Vector3Data[] value;

        public override object GetValue()
        {
            return (object)value;
        }
    }
}
