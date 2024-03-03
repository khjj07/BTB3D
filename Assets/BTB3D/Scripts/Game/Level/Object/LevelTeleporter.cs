using System;
using BTB3D.Scripts.Game.Data;
using BTB3D.Scripts.Game.Level.Object;
using System.Collections;
using System.Collections.Generic;
using BTB3D.Scripts.Game.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelTeleporter : Teleporter
{
    public LevelAsset level;
    public override LevelObjectAsset Serialize(LevelAsset parent)
    {
        var result = base.Serialize(parent);
        result.AddData(parent, AssetDataAsset.Create("level", level as ScriptableObject));
        return result;
    }

    public override void Deserialize(LevelObjectAsset asset)
    {
        base.Deserialize(asset);
        level = asset.GetValue("level") as LevelAsset;
    }

 
    public override void OnInteract()
    {
        var asyncOperation = SceneManager.LoadSceneAsync("Level");
        GameManager.instance.SetLevel(level);
        DOTween.Sequence()
            .Append(GlobalEventManager.instance.FadeOut())
            .AppendCallback(GameManager.instance.LoadLevel)
            .Append(GlobalEventManager.instance.FadeIn());
    }

    public void OnDrawGizmos()
    {
        if (level != null)
        {
            GUIStyle spawnStyle = new GUIStyle();
            spawnStyle.fontSize = 20;
            spawnStyle.alignment = TextAnchor.MiddleCenter;
            spawnStyle.normal.textColor = new Color(1,0.5f,0,1);
            Handles.Label(transform.position + Vector3.up * 3f, level.name, spawnStyle);
        }
    }
}
