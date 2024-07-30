using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace SPlatformer
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _jumpForce = 4f;

        [SerializeField]
        private float _moveSpeed = 5f;

        private Rigidbody2D _rigidbody;

        private bool _isGrounded = true;

        private float _moveDirection;

        private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_moveDirection * _moveSpeed, _rigidbody.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
                _animator.SetBool("isJumping", false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = false;
                _animator.SetBool("isJumping", true);
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
            _animator.SetBool("isRunning", (_moveDirection != 0f));
        }
    }
}