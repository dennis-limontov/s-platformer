using UnityEngine;

namespace SPlatformer
{
    public class MovingGround : MonoBehaviour
    {
        [SerializeField]
        private MovingGroundColliderTop _colliderTop;

        [SerializeField]
        private int _steps = 100;

        [SerializeField]
        private Transform _waypointA;

        [SerializeField]
        private Transform _waypointB;

        private Joint2D _distanceJoint;

        private Rigidbody2D _rigidbody;

        private int _stepNow = 0;
        
        private int _way = -1;

        private void Awake()
        {
            _distanceJoint = GetComponent<Joint2D>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _colliderTop.OnCollisionEnter += Connect;
            _colliderTop.OnCollisionExit += Disconnect;
        }

        private void FixedUpdate()
        {
            Vector2 vector = (_waypointA.position - _waypointB.position) / _steps;
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

            _rigidbody.MovePosition((Vector2)transform.position + vector * _way);
        }

        private void OnDestroy()
        {
            _colliderTop.OnCollisionExit -= Disconnect;
            _colliderTop.OnCollisionEnter -= Connect;
        }

        private void Connect(Rigidbody2D player)
        {
            _distanceJoint.connectedBody = player;
        }

        private void Disconnect(Rigidbody2D player)
        {
            _distanceJoint.connectedBody = null;
        }
    }
}