using UnityEngine;

namespace BTB3D.Scripts.Game.Level.Object
{
    public abstract class LevelObject : SerializableObject<LevelObjectAsset>
    {
        public GameObject modelPrefab;
        public abstract void Initialize(GameObject modelPrefab);
    }
}
