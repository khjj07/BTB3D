using System;
using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Game.Manager;
using BTB3D.Scripts.Interface;
using BTB3D.Scripts.Util;
using DebugExtentionMethods;
using JetBrains.Annotations;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace BTB3D.Scripts.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private KeyCode interactionKey;
        [SerializeField, Range(1.0f, 5.0f)] private float interactRayDistance;
        

        [Header("Camera Option")]
   
        [SerializeField] private Transform cameraHead;
        [SerializeField] private Transform neck;

        [CanBeNull] private IInteractable _lookTarget = null;

        private Camera _camera;
        private Game.Player.Player _player;

        private void Awake()
        {
            _camera = cameraHead.GetComponentInChildren<Camera>();
            _player = GetComponent<Game.Player.Player>();
        }

        private void Start()
        {
           // cameraHead.transform.position = _player.head.transform.position;
            SetCursorOption();
            CreateInteractStream();
            CreateInputStream();
            var groundStream = this.UpdateAsObservable()
                .Where(_ => _player.isGround);
            var notGroundStream = this.UpdateAsObservable()
                .Where(_ => !_player.isGround);

            groundStream.Where(_ => _player.GetAction() == Player.Action.Falling)
                .Subscribe(x =>
                {
                    _player.SetAction(Player.Action.Landing);
                    Observable.Timer(TimeSpan.FromSeconds(0.3f))
                        .Subscribe(_ => _player.SetAction(Player.Action.Idle));
                }).AddTo(gameObject);


            groundStream.Where(_ => _player.GetAction() != Player.Action.Landing && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
                .Subscribe(x =>
                {
                    _player.SetAction(Player.Action.Idle);
                }).AddTo(gameObject);

            notGroundStream.Subscribe(x =>
            {
                if (_player.GetAction() != Player.Action.Landing)
                {
                    _player.SetAction(Player.Action.Falling);
                }
            }).AddTo(gameObject);
        }

        private void CreateInteractStream()
        {
            this.UpdateAsObservable().Select(_ =>
                {
                    var center = new Vector2(Screen.width / 2, Screen.height / 2);
                    Ray ray = _camera.ScreenPointToRay(center);
                    RaycastHit hit;
#if UNITY_EDITOR
                    DebugExtension.DrawBoxCastBox(ray.origin, Vector3.one * 0.3f, Quaternion.LookRotation(ray.direction,cameraHead.transform.up), ray.direction, interactRayDistance,Color.red);
#endif
                    var isContact = Physics.BoxCast(ray.origin,Vector3.one*0.3f, ray.direction, out hit, Quaternion.LookRotation(ray.direction, cameraHead.transform.up), interactRayDistance);
                    if (isContact)
                    {
                        var interactionTarget = hit.collider.GetComponentInParent<LevelObject>() as IInteractable;
                        if (interactionTarget!=null)
                        {
                            GlobalEventManager.instance.ShowInteractionUI(_camera, hit.collider.bounds.center);
                            return interactionTarget;
                        }
                        else
                        {
                            GlobalEventManager.instance.HideInteractionUI();
                            return null;
                        }
                    }
                    else
                    {
                        GlobalEventManager.instance.HideInteractionUI();
                        return null;
                    }
                }).Where(x => x != null)
                .Subscribe(x =>
                {
                    Debug.Log("target");
                    _lookTarget = x;
                }).AddTo(gameObject);
        }

        public void SetPlayerPosition(Vector3 pos)
        {
            _player.transform.position = pos;
        }

        private void CreateInputStream()
        {
            GlobalInputBinder.CreateGetAxisStream("Mouse X").Subscribe(_player.RotateY).AddTo(gameObject);
            GlobalInputBinder.CreateGetAxisStream("Mouse Y").Subscribe(_player.RotateCameraX).AddTo(gameObject);


            var horizontalMovementStream = GlobalInputBinder.CreateGetAxisStream("Horizontal");
            horizontalMovementStream.Subscribe(_player.MoveX).AddTo(gameObject);

            var verticalMovementStream = GlobalInputBinder.CreateGetAxisStream("Vertical");
            verticalMovementStream.Subscribe(_player.MoveZ).AddTo(gameObject);


            GlobalInputBinder.CreateGetAxisStreamOptimize("Jump")
                .Where(_ => _player.isGround)
                .Subscribe(_player.Jump).AddTo(gameObject);


            GlobalInputBinder.CreateGetKeyDownStream(interactionKey)
                .Select(_ => _lookTarget)
                .Where(x => x != null)
                .Subscribe(Interact).AddTo(gameObject);

            this.UpdateAsObservable().Subscribe(_ =>
            {
                cameraHead.transform.localRotation = Quaternion.Euler(_player.rotationX+10.0f, 0, 0);
                cameraHead.transform.transform.position = neck.position;
                //_camera.transform.localPosition = new Vector3(0, 0.004f+_player.GetRotationX()*-0.02f, 0.3f+_player.GetRotationX()*0.008f);
            }).AddTo(gameObject);
        }

        private void Interact(IInteractable target)
        {
            target.OnInteract();
        }

        private void SetCursorOption()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
