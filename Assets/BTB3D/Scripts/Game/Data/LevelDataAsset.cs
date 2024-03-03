using System;
using UnityEngine;

namespace BTB3D.Scripts.Game.Data
{
    [Serializable]
    public class LevelDataAsset : ScriptableObject
    {
        public string name;

        public virtual object GetValue()
        {
            return null;
        }
       
    }

}