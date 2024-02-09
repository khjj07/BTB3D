using System;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.Player
{
    [CustomEditor(typeof(PlayerRecorder))]
    public class PlayerRecorderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PlayerRecorder recorder = (PlayerRecorder)target;
            if (GUILayout.Button("Record Start"))
            {
                if (!recorder.isRecord)
                {
                    recorder.RecordStart();
                }
            }

            if (GUILayout.Button("Record Stop"))
            {
                if (recorder.isRecord)
                {
                    recorder.RecordStop();
                }
            }

            if (GUILayout.Button("Create Clone"))
            {
                var clone = recorder.CreateClone();
                clone.Play();
            }
        }
    }

    public class PlayerRecorder : MonoBehaviour
    {
        [SerializeField] private PlayerClone clonePrefab;
        [SerializeField] private Game.Player.Player target;
        [SerializeField] private bool recordWhenStart;
        [HideInInspector] public bool isRecord;

        [SerializeField,Range(0.0001f,0.1f)] public float recordLatency;
        [SerializeField, Range(0, 100.0f)] private float recordDuration;

        // Start is called before the first frame update
        private List<Vector3> _velocities=new List<Vector3>();
        private List<Game.Player.Player.AnimationState> _animationStates=new List<Game.Player.Player.AnimationState>();
        private List<float> _rotationXList= new List<float>();
        private List<float> _rotationYList= new List<float>();

        public void Start()
        {
            if (recordWhenStart)
            {
                RecordStart();
            }
            
        }

        public void RecordStart()
        {
            isRecord = true;
            _velocities.Clear();
            Observable.Interval(TimeSpan.FromSeconds(recordLatency)).TakeWhile(_ => isRecord).Subscribe(_ =>
            {
                _velocities.Add((target.GetVelocity()));
                _animationStates.Add((target.GetAnimationState()));
                _rotationXList.Add((target.GetRotationX()));
                _rotationYList.Add((target.GetRotationY()));
                recordDuration += recordLatency;
            });
        }

        public void RecordStop()
        {
            isRecord = false;
        }

        public PlayerClone CreateClone()
        {
            PlayerClone clone = Instantiate(clonePrefab);
            clone.latency = recordLatency;
            clone.velocities = _velocities.ToArray();
            clone.rotationXArray = _rotationXList.ToArray();
            clone.rotationYArray = _rotationYList.ToArray();
            clone.animationStates = _animationStates.ToArray();
            return clone;
        }

    }
}
