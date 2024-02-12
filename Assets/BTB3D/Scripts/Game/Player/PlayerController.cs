using System;
using BTB3D.Scripts.Game.Level.Object;
using BTB3D.Scripts.Interface;
using BTB3D.Scripts.Util;
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

        [CanBeNull] private IInteractable _lookTarget = null;

        private Camera _camera;
        private Game.Player.Player _player;


        private void Start()
        {
            _camera = cameraHead.GetComponentInChildren<Camera>();
            _player = GetComponentInChildren<Game.Player.Player>();
           // cameraHead.transform.position = _player.head.transform.position;
            SetCursorOption();
            CreateInteractStream();
            CreateInputStream();
            var groundStream = this.UpdateAsObservable()
                .Where(_ => _player.IsGround());
            var notGroundStream = this.UpdateAsObservable()
                .Where(_ => !_player.IsGround());

            groundStream.Where(_ => _player.GetAnimationState() == Player.AnimationState.Falling)
                .Subscribe(x =>
                {
                    _player.SetAnimationState(Player.AnimationState.Landing);
                    Observable.Timer(TimeSpan.FromSeconds(0.3f))
                        .Subscribe(_ => _player.SetAnimationState(Player.AnimationState.Idle));
                }).AddTo(gameObject);


            groundStream.Where(_ => _player.GetAnimationState() != Player.AnimationState.Landing && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
                .Subscribe(x =>
                {
                    _player.SetAnimationState(Player.AnimationState.Idle);
                }).AddTo(gameObject);

            notGroundStream.Subscribe(x =>
            {
                if (_player.GetAnimationState() != Player.AnimationState.Landing)
                {
                    _player.SetAnimationState(Player.AnimationState.Falling);
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
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * interactRayDistance, Color.red);
#endif
                    var isContact = Physics.Raycast(ray.origin, ray.direction, out hit, interactRayDistance);
                    if (isContact)
                    {
                        return hit.collider.GetComponent<LevelObject>() as IInteractable;
                    }
                    else
                    {
                        return null;
                    }
                }).Where(x => x != null)
                .Subscribe(x =>
                {
                    Debug.Log("target");
                    _lookTarget = x;
                }).AddTo(gameObject);

        }

        private void CreateInputStream()
        {
            GlobalInputBinder.CreateGetAxisStreamOptimize("Mouse X").Subscribe(_player.RotateY).AddTo(gameObject);
            GlobalInputBinder.CreateGetAxisStreamOptimize("Mouse Y").Subscribe(_player.RotateCameraX).AddTo(gameObject);


            var horizontalMovementStream = GlobalInputBinder.CreateGetAxisStreamOptimize("Horizontal");
            horizontalMovementStream.Subscribe(_player.MoveX).AddTo(gameObject);

            var verticalMovementStream = GlobalInputBinder.CreateGetAxisStreamOptimize("Vertical");
            verticalMovementStream.Subscribe(_player.MoveZ).AddTo(gameObject);


            GlobalInputBinder.CreateGetAxisStreamOptimize("Jump")
                .Where(_ => _player.IsGround())
                .Subscribe(_player.Jump).AddTo(gameObject);


            GlobalInputBinder.CreateGetKeyDownStream(interactionKey)
                .Select(_ => _lookTarget)
                .Where(x => x != null)
                .Subscribe(Interact).AddTo(gameObject);

            this.UpdateAsObservable().Subscribe(_ =>
            {
                cameraHead.transform.localRotation = Quaternion.Euler(_player.GetRotationX()+10.0f, 0, 0);
                _camera.transform.localPosition = new Vector3(0, 0.0015f+_player.GetRotationX()*-0.00002f, 0.004f+_player.GetRotationX()*0.00007f);
            });
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
