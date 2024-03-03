using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;

namespace BTB3D.Scripts.Game.Player
{
    public class PlayerRecorder : MonoBehaviour
    {
        [SerializeField] private PlayerClone clonePrefab;
       
        [HideInInspector] public bool isRecord;

        // Start is called before the first frame update
 
    
        private List<Game.Player.Player.Action> _actions=new List<Game.Player.Player.Action>();
     
        private List<float> _moveXList = new List<float>();
        private List<float> _moveZList= new List<float>();
        private List<float> _rotationXList= new List<float>();
        private List<float> _rotationYList= new List<float>();
        private IDisposable recordStream;


        public void RecordStart(Player target)
        {
            isRecord = true;
            recordStream=this.FixedUpdateAsObservable().Subscribe(_ =>
            {
                _actions.Add((target.GetAction()));
                _moveXList.Add(target.moveDirectionX);
                _moveZList.Add(target.moveDirectionZ);
                _rotationXList.Add((target.rotationDirectionX));
                _rotationYList.Add((target.rotationDirectionY));
            });
        }

        public void ResetRecorder()
        {

            _actions.Clear();
            _moveXList.Clear();
            _moveZList.Clear();
            _rotationXList.Clear();
            _rotationYList.Clear();
            
            _actions =new List<Player.Action>();
            _moveXList = new List<float>();
            _moveZList = new List<float>();
            _rotationXList = new List<float>();
            _rotationYList = new List<float>();
        }

        public void RecordStop()
        {
            isRecord = false;
            recordStream.Dispose();
        }

        public PlayerClone SaveClone()
        {
            PlayerClone clone = Instantiate(clonePrefab,transform);
            clone.gameObject.SetActive(false);
            clone.actions = _actions.ToArray();
            clone.rotationDirectionXArray = _rotationXList.ToArray();
            clone.rotationDirectionYArray = _rotationYList.ToArray();
            clone.moveDirectionXArray = _moveXList.ToArray();
            clone.moveDirectionZArray = _moveZList.ToArray();
            return clone;
        }

    }
}
