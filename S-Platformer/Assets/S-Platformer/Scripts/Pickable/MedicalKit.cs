using UnityEngine;

namespace SPlatformer
{
    public class MedicalKit : Pickable
    {
        [field: SerializeField]
        public int Increment { get; private set; }

        [SerializeField]
        private AudioClip _medicalKitSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerHealth>().TryHeal(Increment))
            {
                AudioController.Instance.Play(_medicalKitSound);
                gameObject.SetActive(false);
            }
        }
    }
}