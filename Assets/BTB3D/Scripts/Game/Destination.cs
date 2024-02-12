using BTB3D.Scripts.Data;
using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;
using UnityEngine;

namespace BTB3D.Scripts.Game
{
    public class Destination : SerializableObject<SerializedObject>
    {
        public BoxCollider boxCollider;
        public override SerializedObject Serialize(LevelAsset parent)
        {
            var asset = new SerializedObject();
            asset.name = gameObject.name;
            asset.position = transform.position;
            asset.eulerAngles = transform.eulerAngles;
            asset.scale = transform.localScale;
            asset.AddData(parent,Vector3Data.Create("size", boxCollider.size));
            return asset;
        }

        public override void Deserialize(SerializedObject asset)
        {
            gameObject.name= asset.name;
            transform.position = asset.position;
            transform.eulerAngles = asset.eulerAngles;
            transform.localScale = asset.scale;
            boxCollider.size = (Vector3)asset.GetValue("size");
        }
    }
}
