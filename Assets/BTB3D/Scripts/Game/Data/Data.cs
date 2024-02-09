using System;
using System.Collections.Generic;
using UnityEngine;

namespace BTB3D.Scripts.Data
{
    [Serializable]
    public class Data : ScriptableObject
    {
        public string name;

        public virtual object GetValue()
        {
            return null;
        }
       
    }

}