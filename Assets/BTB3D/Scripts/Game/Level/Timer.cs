using System;
using System.Collections;
using System.Collections.Generic;
using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;
using TMPro;
using UnityEngine;

public class Timer : BaseObject<BaseObjectAsset>
{
    private TextMeshProUGUI _text;

    public void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Set(float seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        _text.SetText(string.Format("{0:D2}:{1:D2}", time.Seconds, time.Milliseconds));
    }

    public override BaseObjectAsset Serialize(LevelAsset parent)
    {
        var asset = new BaseObjectAsset();
        asset.name = gameObject.name;
        asset.position = transform.position;
        asset.eulerAngles = transform.eulerAngles;
        asset.scale = transform.localScale;
        return asset;
    }

    public override void Deserialize(BaseObjectAsset asset)
    {
        gameObject.name = asset.name;
        transform.position = asset.position;
        transform.eulerAngles = asset.eulerAngles;
        transform.localScale = asset.scale;
    }
}
