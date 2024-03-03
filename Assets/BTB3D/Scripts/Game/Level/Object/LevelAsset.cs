using BTB3D.Scripts.Game.ETC;
using UnityEngine;

namespace BTB3D.Scripts.Game.Level.Object
{
    [CreateAssetMenu(fileName = "New Level",menuName = "Level/Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        [Header("Setting")] 
        public float timeLimit;
        public int chance;
        
        [Header("Objects")]
        public BaseObjectAsset spawnPoint;
        public BaseObjectAsset destination;
        public BaseObjectAsset timer;
        public LevelObjectAsset[] objects;
    }
}
