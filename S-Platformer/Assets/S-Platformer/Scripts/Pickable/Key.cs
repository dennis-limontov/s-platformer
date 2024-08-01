using UnityEngine;

namespace SPlatformer
{
    public class Key : Pickable
    {
        [SerializeField]
        private AudioClip _keySound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventHub.OnKeyCollected?.Invoke();
            AudioController.Instance.Play(_keySound);
            gameObject.SetActive(false);
        }
    }
}