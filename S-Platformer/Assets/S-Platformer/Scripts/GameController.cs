using System;
using UnityEngine;

namespace SPlatformer
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private PickableEventChannel _keyChannel;

        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private Transform _startCheckPoint;

        private Transform _currentCheckPoint;

        private int _keysAmount;

        private int _currentKeys;
        private int CurrentKeys
        {
            get { return _currentKeys; }
            set
            {
                _currentKeys = value;
                EventHub.OnKeyChanged?.Invoke(_currentKeys, _keysAmount);
            }
        }

        private void OnDestroy()
        {
            _keyChannel.OnPick -= PickHandler;
            EventHub.OnGameRestarted -= GameRestartedHandler;
            EventHub.OnFallingHappened -= MovePlayerToCheckPoint;
            EventHub.OnCheckPointCrossed -= CheckPointCrossedHandler;
        }

        private void Start()
        {
            GameRestartedHandler();
            _keysAmount = FindObjectsByType<Key>(FindObjectsInactive.Include,
                FindObjectsSortMode.None).Length;
            EventHub.OnCheckPointCrossed += CheckPointCrossedHandler;
            EventHub.OnFallingHappened += MovePlayerToCheckPoint;
            EventHub.OnGameRestarted += GameRestartedHandler;
            _keyChannel.OnPick += PickHandler;
            EventHub.OnKeyChanged?.Invoke(CurrentKeys, _keysAmount);
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

        private void PickHandler(Pickable pickable)
        {
            CurrentKeys++;
            pickable.gameObject.SetActive(false);
        }
    }
}