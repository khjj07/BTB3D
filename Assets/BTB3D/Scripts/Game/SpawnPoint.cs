using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level.Object;

namespace BTB3D.Scripts.Game
{
    public class SpawnPoint : SerializableObject<SerializedObject>
    {
        public override SerializedObject Serialize(LevelAsset parent)
        {
            var asset = new SerializedObject();
            asset.name = gameObject.name;
            asset.position = transform.position;
            asset.eulerAngles = transform.eulerAngles;
            asset.scale = transform.localScale;
            return asset;
        }

        public override void Deserialize(SerializedObject asset)
        {
            gameObject.name = asset.name;
            transform.position = asset.position;
            transform.eulerAngles = asset.eulerAngles;
            transform.localScale = asset.scale;
        }
    }
}
