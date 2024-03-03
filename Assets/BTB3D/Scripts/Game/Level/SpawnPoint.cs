using BTB3D.Scripts.Game.ETC;
using BTB3D.Scripts.Game.Level;
using BTB3D.Scripts.Game.Level.Object;
using UnityEditor;
using UnityEngine;
namespace BTB3D.Scripts.Game
{
    public class SpawnPoint : BaseObject<BaseObjectAsset>
    {
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawMesh(GlobalLevelSetting.instance.playerMesh, transform.position,
                transform.rotation, Vector3.one * 100);
            GUIStyle spawnStyle = new GUIStyle();
            spawnStyle.fontSize = 20;
            spawnStyle.alignment = TextAnchor.MiddleCenter;
            spawnStyle.normal.textColor = Color.green;
            Handles.Label(transform.position + Vector3.up * 2.5f, "Spawn Point", spawnStyle);
        }
    }
}
