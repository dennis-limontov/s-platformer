using UnityEngine;

namespace SPlatformer
{
    public class LevelFinish : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventHub.OnGameRestarted?.Invoke();
        }
    }
}