using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;

namespace BTB3D.Scripts.Game.Player
{
    public class Player : MonoBehaviour
    {
        public enum AnimationState
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

        public AnimationState currentAnimationState = AnimationState.Idle;
        private Vector3 _velocity;

        private bool _isGround;

        private float _rotationX;
        private float _rotationY;


        public bool IsGround()
        {
            return _isGround;
        }

        public Vector3 GetVelocity()
        {
            return _velocity;
        }

        public void SetAnimationState(AnimationState state)
        {
            currentAnimationState = state;
        }

        public AnimationState GetAnimationState()
        {
            return currentAnimationState;
        }

        public void Jump(float value)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (_isGround)
            {
                SetAnimationState(AnimationState.Jump);
            }
        }


        private void Animate()
        {
            _animator.SetInteger("State", (int)currentAnimationState);
        }

        private void CreateHeadMovementStream()
        {
            head.UpdateAsObservable().Subscribe(_ =>
            {
                head.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
            }).AddTo(gameObject);
        }

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            var checkGroundStream = this.UpdateAsObservable()
                .Select(_ => Physics.Raycast(transform.position + Vector3.up * (groundCheckRayDistance / 2), Vector3.down, groundCheckRayDistance));

            var isGroundStream = checkGroundStream.Where(x => x);
            checkGroundStream.Subscribe(x => { _isGround = x; });

            CreateHeadMovementStream();
        }

        private void Update()
        {
            _velocity = _rigidbody.velocity;
            Animate();

#if UNITY_EDITOR
            if (_isGround)
            {
                Debug.DrawLine(transform.position + Vector3.up * (groundCheckRayDistance / 2), transform.position + Vector3.down * groundCheckRayDistance, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position + Vector3.up * (groundCheckRayDistance / 2), transform.position + Vector3.down * groundCheckRayDistance);
            }
#endif
        }


        public void RotateY(float direction)
        {
            _rotationY += direction * sensitiveX;
            if (_isGround)
            {
                if (direction > 0)
                {
                    SetAnimationState(AnimationState.TurnRight);
                }
                else
                {
                    SetAnimationState(AnimationState.TurnLeft);
                }
            }
        }

        public void RotateCameraX(float direction)
        {
            _rotationX -= direction * sensitiveY;
            _rotationX = Mathf.Clamp(_rotationX, minRotationX, maxRotationX);
        }

        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void MoveX(float direction)
        {
            _rigidbody.AddForce(transform.right * direction * walkSpeed * Time.deltaTime, ForceMode.VelocityChange);
            if (_isGround)
            {
                if (direction > 0)
                {
                    SetAnimationState(AnimationState.WalkRight);
                }
                else
                {
                    SetAnimationState(AnimationState.WalkLeft);
                }
            }
        }

        public void MoveZ(float direction)
        {
            _rigidbody.AddForce(transform.forward * direction * walkSpeed * Time.deltaTime, ForceMode.VelocityChange);
            if (_isGround)
            {
                if (direction > 0)
                {
                    SetAnimationState(AnimationState.WalkForward);
                }
                else
                {
                    SetAnimationState(AnimationState.WalkBackward);
                }
            }
        }
        public void SetRotationX(float value)
        {
            _rotationX = value;
        }

        public void SetRotationY(float value)
        {
            _rotationY = value;
        }

        public float GetRotationX()
        {
            return _rotationX;
        }

        public float GetRotationY()
        {
            return _rotationY;
        }
    }
}
