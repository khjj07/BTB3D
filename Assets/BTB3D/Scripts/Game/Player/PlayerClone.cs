using System;
using UniRx;
using UnityEngine;

namespace BTB3D.Scripts.Game.Player
{
    public class PlayerClone : MonoBehaviour
    {
        public float latency;
        public Vector3[] velocities;
        public Game.Player.Player.AnimationState[] animationStates;
        public float[] rotationXArray;
        public float[] rotationYArray;
        private int index = 0;
        private Game.Player.Player _player;

        private void Start()
        {
            _player = GetComponentInChildren<Game.Player.Player>();
        }

        public void Play()
        {
            Observable.Interval(TimeSpan.FromSeconds(latency)).TakeWhile(_=> index<velocities.Length).Subscribe(_ =>
            {
                _player.Move(velocities[index]);
                _player.SetAnimationState(animationStates[index]);
                _player.SetRotationX(rotationXArray[index]);
                _player.SetRotationY(rotationYArray[index]);
                index++;
            });
        }
    }
}
