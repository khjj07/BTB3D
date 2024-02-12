using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;
using UnityEngine;

namespace BTB3D.Scripts.Game
{
    public abstract class SerializableObject<T> : MonoBehaviour where T : SerializedObject
    {
        public abstract T Serialize(LevelAsset parent);
        public abstract void Deserialize(T asset);

        public virtual void PostDeserialize(T asset)
        {

        }

    }
}
