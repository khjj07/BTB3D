using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.ETC
{
    [Serializable]
    public class SerializedObject
    {
        public string name;
        public Vector3 position;
        public Vector3 eulerAngles;
        public Vector3 scale;
        public List<Scripts.Data.Data> data = new List<Scripts.Data.Data>();
        public object GetValue(string name)
        {
            return data.Find(x => x.name == name).GetValue();
        }

        public Scripts.Data.Data GetData(string name)
        {
            return data.Find(x => x.name == name);
        }

        public void AddData(ScriptableObject parent, Scripts.Data.Data value)
        {
            data.Add(value);
            AssetDatabase.AddObjectToAsset(value, parent);
        }
    }
}
