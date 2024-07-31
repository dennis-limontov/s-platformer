using UnityEngine;

namespace SPlatformer
{
    public class Key : Pickable
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventHub.OnKeyCollected?.Invoke();
            gameObject.SetActive(false);
        }
    }
}