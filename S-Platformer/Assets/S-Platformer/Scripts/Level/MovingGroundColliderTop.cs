using System;
using UnityEngine;

namespace SPlatformer
{
    public class MovingGroundColliderTop : MonoBehaviour
    {
        public event Action<Rigidbody2D> OnCollisionEnter;
        public event Action<Rigidbody2D> OnCollisionExit;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(collision.rigidbody);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExit?.Invoke(collision.rigidbody);
        }
    }
}