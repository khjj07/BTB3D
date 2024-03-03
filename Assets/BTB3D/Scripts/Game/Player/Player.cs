using BTB3D.Scripts.Interface;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;

namespace BTB3D.Scripts.Game.Player
{
    public class Player : MonoBehaviour, IDynamic
    {
        public enum Action
        {
            Idle,
            WalkForward,
            WalkBackward,
            WalkLeft,
            WalkRight,
            Jump,
            Falling,
            Landing,
            TurnLeft,
            TurnRight,
        }

        [SerializeField, Range(0.1f, 50.0f)] private float walkSpeed;
        [SerializeField, Range(0.1f, 10.0f)] private float jumpForce;
        [SerializeField, Range(0.1f, 5.0f)] private float groundCheckRayDistance;

        [SerializeField, Range(1.0f, 5.0f)] private float sensitiveX;
        [SerializeField, Range(1.0f, 5.0f)] private float sensitiveY;

        [SerializeField] private float maxRotationX = 90.0f;
        [SerializeField] private float minRotationX = -90.0f;

        public Transform head;

        private Animator _animator;
        private Rigidbody _rigidbody;


        public Action currentAction = Action.Idle;
        public bool isGround;
        public float rotationX;
        public float rotationY;

        public float moveDirectionX;
        public float moveDirectionZ;
        public float rotationDirectionX;
        public float rotationDirectionY;

        public void SetAction(Action action)
        {
            currentAction = action;
        }

        public Action GetAction()
        {
            return currentAction;
        }

        public void RotateY(float direction)
        {
            rotationDirectionY = direction;
            rotationY += direction * sensitiveX;
            if (isGround)
            {
                if (direction > 0.2f && (currentAction != Action.Falling || currentAction != Action.Landing))
                {
                    SetAction(Action.TurnRight);
                }
                else if (direction < -0.2f && (currentAction != Action.Falling || currentAction != Action.Landing))
                {
                    SetAction(Action.TurnLeft);
                }
            }
        }

        public void RotateCameraX(float direction)
        {
            rotationDirectionX = direction;
            rotationX -= direction * sensitiveY;
            rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        }

        public void MoveX(float direction)
        {
            moveDirectionX = direction;
            _rigidbody.AddForce(transform.right * direction * walkSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //transform.Translate(transform.right * direction * walkSpeed * Time.fixedDeltaTime, Space.World);
            if (isGround)
            {
                if (direction > 0 && (currentAction != Action.Falling || currentAction != Action.Landing))
                {
                    SetAction(Action.WalkRight);
                }
                else
                {
                    SetAction(Action.WalkLeft);
                }
            }
        }

        public void MoveZ(float direction)
        {
            moveDirectionZ = direction;
            _rigidbody.AddForce(transform.forward * direction * walkSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //transform.Translate(transform.forward * direction * walkSpeed * Time.fixedDeltaTime, Space.World);
            if (isGround)
            {
                if (direction > 0 && (currentAction != Action.Falling || currentAction != Action.Landing))
                {
                    SetAction(Action.WalkForward);
                }
                else
                {
                    SetAction(Action.WalkBackward);
                }
            }
        }

        public void Jump(float value)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (isGround)
            {
                SetAction(Action.Jump);
            }
        }

        private void Animate()
        {
            _animator.SetInteger("State", (int)currentAction);
        }

        public void CreateHeadMovementStream()
        {
            head.UpdateAsObservable().Subscribe(_ =>
            {
                head.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.localRotation = Quaternion.Euler(0, rotationY, 0);
            }).AddTo(gameObject);
        }

        public void CreateCheckGroundStream()
        {
            var checkGroundStream = this.UpdateAsObservable()
                .Select(_ => Physics.Raycast(transform.position + Vector3.up * (groundCheckRayDistance / 2), Vector3.down, groundCheckRayDistance));

            var isGroundStream = checkGroundStream.Where(x => x);
            checkGroundStream.Subscribe(x => { isGround = x; })
                .AddTo(gameObject);
        }

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            CreateHeadMovementStream();
            CreateCheckGroundStream();
        }

        private void Update()
        {
            Animate();

#if UNITY_EDITOR
            if (isGround)
            {
                Debug.DrawLine(transform.position + Vector3.up * (groundCheckRayDistance / 2), transform.position + Vector3.down * groundCheckRayDistance, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position + Vector3.up * (groundCheckRayDistance / 2), transform.position + Vector3.down * groundCheckRayDistance);
            }
#endif
        }
    }
}
