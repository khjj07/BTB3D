using System;
using System.Collections.Generic;
using BTB3D.Scripts.Interface;
using UnityEditor;
using UnityEngine;
using SerializedObject = BTB3D.Scripts.Game.ETC.SerializedObject;

namespace BTB3D.Scripts.Game.Level.Object
{
    [Serializable]
    public class LevelObjectAsset:SerializedObject
    {
        public LevelObject prefab;
        public GameObject modelPrefab;

        //~LevelObjectAsset()
        //{
        //    var objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
        //
        //    foreach (var child in objects)
        //    {
        //        AssetDatabase.RemoveObjectFromAsset(child);
        //    }
        //}
    }
}
