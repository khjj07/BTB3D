using BTB3D.Scripts.Data;
using UnityEditor;

namespace BTB3D.Scripts.Game.Data
{
    public class PointArrayDataAsset : LevelDataAsset
    {
        public static LevelDataAsset Create(string name, Vector3DataAsset[] value)
        {
            var instance = CreateInstance<PointArrayDataAsset>();
            EditorUtility.SetDirty(instance);
            instance.name = name;
            instance.value = value;
            foreach (var child in value)
            {
                
            }
            return instance;
        }
        public Vector3DataAsset[] value;

        public override object GetValue()
        {
            return (object)value;
        }
    }
}
