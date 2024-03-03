using BTB3D.Scripts.Game.Player;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace BTB3D.Scripts.Game.Manager
{
    public class LobbyManager : MonoBehaviour
    {
        public PlayerController playerPrefab;
        public SpawnPoint spawnPoint;
        public Transform origin;

        public GameManager gameManagerPrefab;
        public GlobalEventManager globalEventManagerPrefab;

        public void SpawnPlayer()
        {
            var player = Instantiate(playerPrefab, origin);
            player.SetPlayerPosition(spawnPoint.transform.position);
            player.transform.rotation = spawnPoint.transform.rotation;
        }

        public void Awake()
        {
            if (!GameManager.instance)
            {
                Instantiate(gameManagerPrefab);
            }
            if (!GlobalEventManager.instance)
            {
                Instantiate(globalEventManagerPrefab);
            }
        }
        public void Start()
        {
            SpawnPlayer();
        }
    }
}
