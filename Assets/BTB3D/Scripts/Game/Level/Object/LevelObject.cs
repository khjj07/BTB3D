using System;
using BTB3D.Scripts.Game.ETC;
using UnityEngine;

namespace BTB3D.Scripts.Game.Level.Object
{
    [Serializable]
    public class LevelObjectAsset : BaseObjectAsset
    {
        public LevelObject prefab;
        public GameObject modelPrefab;
    }

    public abstract class LevelObject : BaseObject<LevelObjectAsset>
    {
       
        public GameObject modelPrefab;
        public abstract void Initialize(GameObject modelPrefab);
    }
}
