using System;
using BTB3D.Scripts.Data;
using UnityEditor;

namespace BTB3D.Scripts.Game.Data
{
    [Serializable]
    public class BoolDataAsset : LevelDataAsset
    {
        public static LevelDataAsset Create(string name, bool value)
        {
            var instance = CreateInstance<BoolDataAsset>();
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