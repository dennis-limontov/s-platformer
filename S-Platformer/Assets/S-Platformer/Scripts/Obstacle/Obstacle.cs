using UnityEngine;

namespace SPlatformer
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private int _hurtAmount = 1;

        private void OnTriggerStay2D(Collider2D collision)
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().Hurt(_hurtAmount);
        }
    }
}