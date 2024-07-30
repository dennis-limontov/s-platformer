using UnityEngine;

namespace SPlatformer
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField]
        private PickableEventChannel _channel;

        private void Awake()
        {
            EventHub.OnGameRestarted += GameRestartedHandler;
        }

        private void GameRestartedHandler()
        {
            gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            EventHub.OnGameRestarted -= GameRestartedHandler;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _channel.OnPick?.Invoke(this);
        }
    }
}