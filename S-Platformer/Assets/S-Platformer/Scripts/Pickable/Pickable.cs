using UnityEngine;

namespace SPlatformer
{
    public class Pickable : MonoBehaviour
    {
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
    }
}