using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace BTB3D.Scripts.Game.Player
{
    public class PlayerClone : MonoBehaviour
    {
        public Game.Player.Player.Action[] actions;
        public float[] moveDirectionXArray;
        public float[] moveDirectionZArray;
        public float[] rotationDirectionXArray;
        public float[] rotationDirectionYArray;
        private int index = 0;
        private Game.Player.Player _player;

        private void Start()
        {
            _player = GetComponent<Game.Player.Player>();
        }

        public void Play()
        {
            this.FixedUpdateAsObservable().TakeWhile(_=> index< moveDirectionXArray.Length).Subscribe(_ =>
            {
                _player.MoveX(moveDirectionXArray[index]);
                _player.MoveZ(moveDirectionZArray[index]);
                _player.RotateCameraX(rotationDirectionXArray[index]);
                _player.RotateY(rotationDirectionYArray[index]);
                _player.Act(actions[index]);
                Debug.Log(moveDirectionZArray[index]);
                index++;
            }).AddTo(gameObject);
        }
    }
}
