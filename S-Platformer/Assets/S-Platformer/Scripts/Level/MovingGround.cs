using UnityEngine;

namespace SPlatformer
{
    public class MovingGround : MonoBehaviour
    {
        [SerializeField]
        private MovingGroundColliderTop _colliderTop;

        [SerializeField]
        private int _steps = 300;

        [SerializeField]
        private Transform _waypointA;

        [SerializeField]
        private Transform _waypointB;

        private Joint2D _distanceJoint;

        private Rigidbody2D _rigidbody;

        private int _stepNow = 0;

        private int _way = -1;

        private Vector2 _speed;

        private void Awake()
        {
            _distanceJoint = GetComponent<Joint2D>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _colliderTop.OnCollisionEnter += Connect;
            _colliderTop.OnCollisionExit += Disconnect;
        }

        private void FixedUpdate()
        {
            _speed = (_waypointA.position - _waypointB.position) / _steps;
            if (_stepNow == _steps)
            {
                _stepNow = 0;
                _way *= -1;
                if (_distanceJoint.connectedBody)
                {
                    //_distanceJoint.connectedBody.velocity = Vector2.zero;
                    _distanceJoint.connectedBody.velocity *= -1f;
                }
            }
            _stepNow++;

            _rigidbody.MovePosition((Vector2)transform.position + _speed * _way);
        }

        private void OnDestroy()
        {
            _colliderTop.OnCollisionExit -= Disconnect;
            _colliderTop.OnCollisionEnter -= Connect;
        }

        private void Connect(Rigidbody2D player)
        {
            _distanceJoint.connectedBody = player;
            _distanceJoint.connectedBody.GetComponent<PlayerMovement>().OnSpeedCalculated
                += SpeedCalculatedHandler;
        }

        private void Disconnect(Rigidbody2D player)
        {
            _distanceJoint.connectedBody.GetComponent<PlayerMovement>().OnSpeedCalculated
                -= SpeedCalculatedHandler;
            _distanceJoint.connectedBody = null;
        }

        private void SpeedCalculatedHandler()
        {
            Vector2 localVelocity = _distanceJoint.connectedBody.velocity;
            localVelocity.x += _speed.x * _way / Time.fixedDeltaTime;
            _distanceJoint.connectedBody.velocity = localVelocity;
        }
    }
}