using System;
using System.Collections.Generic;
using BTB3D.Scripts.Data;
using BTB3D.Scripts.Game.Data;
using BTB3D.Scripts.Game.Level.Object;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.ETC
{
    [Serializable]
    public class BaseObjectAsset
    {
        public string name;
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector3 scale;
        public List<LevelDataAsset> data = new List<LevelDataAsset>();
        public object GetValue(string name)
        {
            return data.Find(x => x.name == name).GetValue();
        }

        public LevelDataAsset GetData(string name)
        {
            return data.Find(x => x.name == name);
        }

        public void AddData(ScriptableObject parent, LevelDataAsset value)
        {
            data.Add(value);
            AssetDatabase.AddObjectToAsset(value, parent);
        }
    }

    public abstract class BaseObject<T> : MonoBehaviour where T : BaseObjectAsset
    {
        public abstract T Serialize(LevelAsset parent);

        public abstract void Deserialize(T asset);

        public virtual void PostDeserialize(T asset)
        {
            //throw new System.NotImplementedException();
        }
    }
}
