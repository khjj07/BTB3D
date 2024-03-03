using BTB3D.Scripts.Data;
using UnityEditor;

namespace BTB3D.Scripts.Game.Data
{
    public class StringDataAsset : LevelDataAsset
    {

        public static LevelDataAsset Create(string name, string value)
        {
            var instance = CreateInstance<StringDataAsset>();
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
}
