using BTB3D.Scripts.Game.Level;
using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Game.Player;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.Manger
{
    public class GameManager : MonoBehaviour
    {
        public LevelAsset levelAsset;
        public PlayerController playerPrefab;
        public SpawnPoint spawnPoint;
        public Destination destination;
        public Transform origin;
        public void LoadLevel()
        {
            spawnPoint.Deserialize(levelAsset.spawnPoint);
            destination.Deserialize(levelAsset.destination);
            foreach (var obj in levelAsset.objects)
            {
                var instance = PrefabUtility.InstantiatePrefab(obj.prefab, MapTool.instance.origin) as LevelObject;
                if (instance != null)
                {
                    instance.Deserialize(obj);
                }
            }
        }

        public void Start()
        {
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab, origin);
            player.transform.position = spawnPoint.transform.position;

        }
    }
}
