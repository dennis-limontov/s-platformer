using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace SPlatformer
{
    public class PlayerMovement : MonoBehaviour
    {
        private const string ANIMATION_JUMP = "isJumping";
        private const string ANIMATION_RUN = "isRunning";

        [SerializeField]
        private float _jumpForce = 4f;

        [SerializeField]
        private float _moveSpeed = 5f;

        private Rigidbody2D _rigidbody;

        private bool _isGrounded = true;

        private float _moveDirection;

        private Animator _animator;

        private int _layerGroundNumber;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            _layerGroundNumber = LayerMask.NameToLayer("Ground");
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_moveDirection * _moveSpeed, _rigidbody.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == _layerGroundNumber)
            {
                _isGrounded = true;
                _animator.SetBool(ANIMATION_JUMP, false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == _layerGroundNumber)
            {
                _isGrounded = false;
                _animator.SetBool(ANIMATION_JUMP, true);
            }
        }

        public void OnJump(CallbackContext callbackContext)
        {
            if (_isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        public void OnMove(CallbackContext callbackContext)
        {
            _moveDirection = callbackContext.ReadValue<float>();
            if (_moveDirection < 0f)
            {
                Quaternion rot = transform.rotation;
                rot.y = 180f;
                transform.rotation = rot;
            }
            else if (_moveDirection > 0f)
            {
                transform.rotation = Quaternion.identity;
            }
            _animator.SetBool(ANIMATION_RUN, (_moveDirection != 0f));
        }
    }
}