using System;
using System.Collections.Generic;
using BTB3D.Scripts.Game.Level;
using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Game.Player;
using BTB3D.Scripts.Util;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using Object = UnityEngine.Object;

namespace BTB3D.Scripts.Game.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerController playerPrefab;
        public SpawnPoint spawnPointPrefab;
        public Destination destinationPrefab;
        public Timer timerPrefab;
        public Transform worldOrigin;


        private PlayerController _player;

        [SerializeField]
        private LevelAsset levelAsset;

        private SpawnPoint _spawnPoint;
        private Destination _destination;
        private Timer _timer;

        private PlayerRecorder _recorder;
        private List<PlayerClone> _currentCloneList=new List<PlayerClone>();
        private int _chance;
        private float _timeLimit;


        public void LoadLevel()
        {
            worldOrigin = GameObject.Find("WorldOrigin").transform;
            _spawnPoint = Instantiate(spawnPointPrefab);
            _spawnPoint.Deserialize(levelAsset.spawnPoint);
            _destination = Instantiate(destinationPrefab);
            _destination.Deserialize(levelAsset.destination);
            _timer = Instantiate(timerPrefab, worldOrigin);
            _timer.Deserialize(levelAsset.timer);
            _timeLimit = levelAsset.timeLimit;
            _chance = levelAsset.chance;
            _timer.Set(_timeLimit);

            Debug.Log("LoadLevel");

            List<LevelObject> instances = new List<LevelObject>();
            foreach (var obj in levelAsset.objects)
            {
                var instance = Instantiate(obj.prefab, worldOrigin) as LevelObject;
                if (instance != null)
                {
                    instance.Deserialize(obj);
                    instances.Add(instance);
                }
            }
            for (int i = 0; i < instances.Count; i++)
            {
                instances[i].PostDeserialize(levelAsset.objects[i]);
            }
            SpawnPlayer();
        }

        public void SetLevel(LevelAsset level)
        {
            levelAsset = level;
        }

        public void SpawnPlayer()
        {
            _player = Instantiate(playerPrefab, worldOrigin);
            _player.SetPlayerPosition(_spawnPoint.transform.position);
            _player.transform.rotation = _spawnPoint.transform.rotation;
        }
        public void SpawnClone(PlayerClone clone)
        {
            var newClone = Instantiate(clone, worldOrigin);
            newClone.gameObject.SetActive(true);
            newClone.transform.position = _spawnPoint.transform.position;
            newClone.transform.rotation = _spawnPoint.transform.rotation;
            _currentCloneList.Add(newClone);
            newClone.Play();
        }

        public void RespawnAllClone()
        {
            foreach (var clone in _currentCloneList)
            {
                Destroy(clone.gameObject);
            }

            _currentCloneList = new List<PlayerClone>();

            foreach (var clone in GetComponentsInChildren<PlayerClone>(true))
            {
                SpawnClone(clone);
            }
        }

        public void RespawnPlayer()
        {
            Destroy(_player.gameObject);
            SpawnPlayer();
        }

        public void RoundStart()
        {
            if (_chance > 0)
            {
                _chance--;
                RespawnPlayer();
                RespawnAllClone();
                _recorder.RecordStart(_player.GetComponent<Player.Player>());
                _timeLimit = levelAsset.timeLimit;

                this.FixedUpdateAsObservable()
                    .TakeWhile(_ => _timeLimit > 0)
                    .Subscribe(_ =>
                    {
                        _timeLimit-=Time.fixedDeltaTime;
                        _timer.Set(_timeLimit);
                    }, () =>
                    {
                        _recorder.SaveClone();
                        _recorder.RecordStop();
                        _recorder.ResetRecorder();
                        RoundStart();
                    });
            }
            else
            {
                GameEnd();
            }
        }

        public void GameEnd()
        {
            Debug.Log("game end");
        }

        public void GameStart()
        {
            RoundStart();
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _recorder = GetComponent<PlayerRecorder>();
        }
    }
}
