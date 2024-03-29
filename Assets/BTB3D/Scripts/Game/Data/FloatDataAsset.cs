using System;
using BTB3D.Scripts.Data;
using UnityEditor;

namespace BTB3D.Scripts.Game.Data
{
    [Serializable]
    public class FloatDataAsset : LevelDataAsset
    {

        public override object GetValue()
        {
            return (object)value;
        }
        public static LevelDataAsset Create(string name, float value)
        {
            var instance = CreateInstance<FloatDataAsset>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            return instance;
        }
        public float value;
    }
}