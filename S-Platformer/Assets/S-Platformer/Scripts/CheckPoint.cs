using UnityEngine;

namespace SPlatformer
{
    public class CheckPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventHub.OnCheckPointCrossed?.Invoke(transform);
        }
    }
}