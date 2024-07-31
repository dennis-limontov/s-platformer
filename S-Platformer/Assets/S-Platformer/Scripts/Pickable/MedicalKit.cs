using UnityEngine;

namespace SPlatformer
{
    public class MedicalKit : Pickable
    {
        [field: SerializeField]
        public int Increment { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerHealth>().TryHeal(Increment))
            {
                gameObject.SetActive(false);
            }
        }
    }
}