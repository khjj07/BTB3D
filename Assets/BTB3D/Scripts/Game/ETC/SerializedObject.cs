using System;
using BTB3D.Scripts.Game.Level.Object;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game
{
    [Serializable]
    public class SerializedObject
    {
        public string name;
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector3 scale;
        public List<Data.Data> data = new List<Data.Data>();
        public object GetValue(string name)
        {
            return data.Find(x => x.name == name).GetValue();
        }

        public Data.Data GetData(string name)
        {
            return data.Find(x => x.name == name);
        }

        public void AddData(ScriptableObject parent, Data.Data value)
        {
            data.Add(value);
            AssetDatabase.AddObjectToAsset(value, parent);
        }
    }
}
