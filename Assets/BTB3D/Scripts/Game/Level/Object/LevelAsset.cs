using UnityEngine;

namespace BTB3D.Scripts.Game.Level.Object
{
    [CreateAssetMenu(fileName = "New Level",menuName = "Level/Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        public SerializedObject spawnPoint;
        public SerializedObject destination;
        public LevelObjectAsset[] objects;
    }
}
