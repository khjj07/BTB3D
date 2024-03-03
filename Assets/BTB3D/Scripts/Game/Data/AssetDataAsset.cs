using BTB3D.Scripts.Data;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.Data
{
    public class AssetDataAsset : LevelDataAsset
    {
        public static LevelDataAsset Create(string name, ScriptableObject value)
        {
            var instance = CreateInstance<AssetDataAsset>();
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
}
