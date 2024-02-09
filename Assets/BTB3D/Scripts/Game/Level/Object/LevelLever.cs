using System;
using BTB3D.Scripts.Interface;
using UnityEngine;

namespace BTB3D.Scripts.Game.Level.Object
{
    public class LevelLever : StaticLevelObject,IInteractable
    {
        public LevelObject target;
        public override LevelObjectAsset Serialize(LevelAsset parent)
        {
            var result = base.Serialize(parent);
            result.AddData(parent,StringData.Create("target",target.gameObject.name));
            return result;
        }

        public override void PostDeserialize(LevelObjectAsset asset)
        {
            var targetName = (string)asset.GetValue("target");
            target = GameObject.Find(targetName).GetComponent<LevelObject>();
        }

        public void OnInteract()
        {
            var t =  target as IReactable;
            if (t != null)
            {
                t.React();
            }
        }

        public void OnDrawGizmos()
        {
            if (target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position,target.transform.position);
            }
        }
    }
}
