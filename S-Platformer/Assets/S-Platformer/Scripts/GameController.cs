using UnityEngine;

namespace SPlatformer
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private Transform _startCheckPoint;

        [SerializeField]
        private AudioClip _victorySound;

        private Transform _currentCheckPoint;

        public int KeysAmount { get; private set; }

        private int _currentKeys;
        private int CurrentKeys
        {
            get { return _currentKeys; }
            set
            {
                _currentKeys = value;
                EventHub.OnKeyChanged?.Invoke(_currentKeys, KeysAmount);
            }
        }

        private void Awake()
        {
            EventHub.OnCheckPointCrossed += CheckPointCrossedHandler;
            EventHub.OnFallingHappened += MovePlayerToCheckPoint;
            EventHub.OnGameRestarted += GameRestartedHandler;
            EventHub.OnFinishReached += FinishReachedHandler;
            EventHub.OnKeyCollected += KeyCollectedHandler;

            KeysAmount = FindObjectsByType<Key>(FindObjectsInactive.Include,
                FindObjectsSortMode.None).Length;
        }

        private void OnDestroy()
        {
            EventHub.OnKeyCollected -= KeyCollectedHandler;
            EventHub.OnFinishReached -= FinishReachedHandler;
            EventHub.OnGameRestarted -= GameRestartedHandler;
            EventHub.OnFallingHappened -= MovePlayerToCheckPoint;
            EventHub.OnCheckPointCrossed -= CheckPointCrossedHandler;
        }

        private void Start()
        {
            GameRestartedHandler();
            
            EventHub.OnKeyChanged?.Invoke(CurrentKeys, KeysAmount);
        }

        private void CheckPointCrossedHandler(Transform transform)
        {
            _currentCheckPoint = transform;
        }

        private void MovePlayerToCheckPoint(int hurtAmount)
        {
            _player.transform.position = _currentCheckPoint.position;
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        private void GameRestartedHandler()
        {
            _currentCheckPoint = _startCheckPoint;
            MovePlayerToCheckPoint(0);
            CurrentKeys = 0;
        }

        private void FinishReachedHandler()
        {
            if (_currentKeys == KeysAmount)
            {
                AudioController.Instance.Play(_victorySound);
                EventHub.OnGameRestarted?.Invoke();
            }
        }

        private void KeyCollectedHandler()
        {
            CurrentKeys++;
        }
    }
}