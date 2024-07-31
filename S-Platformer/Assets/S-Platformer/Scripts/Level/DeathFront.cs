using UnityEngine;

namespace SPlatformer
{
    public class DeathFront : MonoBehaviour
    {
        [SerializeField]
        private int _fallingHealthCost = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventHub.OnFallingHappened?.Invoke(_fallingHealthCost);
        }
    }
}