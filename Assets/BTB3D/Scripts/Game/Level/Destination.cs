using System;
using BTB3D.Scripts.Data;
using BTB3D.Scripts.Game.Data;
using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game
{
    public class Destination : BaseObject<BaseObjectAsset>
    {
        public BoxCollider boxCollider;
        public override BaseObjectAsset Serialize(LevelAsset parent)
        {
            var asset = new BaseObjectAsset();
            asset.name = gameObject.name;
            asset.position = transform.position;
            asset.eulerAngles = transform.eulerAngles;
            asset.scale = transform.localScale;
            asset.AddData(parent,Vector3DataAsset.Create("size", boxCollider.size));
            return asset;
        }

        public override void Deserialize(BaseObjectAsset asset)
        {
            gameObject.name= asset.name;
            transform.position = asset.position;
            transform.eulerAngles = asset.eulerAngles;
            transform.localScale = asset.scale;
            boxCollider.size = (Vector3)asset.GetValue("size");
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = new Vector4(1, 0.5f, 0, 0.5f);
            Gizmos.DrawCube(transform.position, boxCollider.size);
            //큐브메쉬로 수정 필요
            GUIStyle destinationStyle = new GUIStyle();
            destinationStyle.fontSize = 20;
            destinationStyle.alignment = TextAnchor.MiddleCenter;
            destinationStyle.normal.textColor = new Vector4(1, 0.5f, 0, 1);
            Handles.Label(transform.position + Vector3.up * (boxCollider.size.y), "Destination", destinationStyle);

        }
    }
}
