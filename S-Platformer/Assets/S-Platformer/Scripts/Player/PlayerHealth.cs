using System.Collections;
using UnityEngine;

namespace SPlatformer
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private float _invulnerableTime = 3f;

        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private AudioClip _gameOverSound;

        [SerializeField]
        private AudioClip _hurtSound;

        private bool _isInvulnerable = false;

        private int _currentHealth;
        private int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
                EventHub.OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
            }
        }

        private void Awake()
        {
            EventHub.OnFallingHappened += Hurt;
            EventHub.OnGameRestarted += Start;
        }

        private void OnDestroy()
        {
            EventHub.OnGameRestarted -= Start;
            EventHub.OnFallingHappened -= Hurt;
        }

        private void Start()
        {
            CurrentHealth = _maxHealth;
        }

        public bool TryHeal(int healAmount)
        {
            if (CurrentHealth < _maxHealth)
            {
                CurrentHealth += healAmount;
                return true;
            }
            return false;
        }

        public void Hurt(int hurtAmount)
        {
            if (!_isInvulnerable)
            {
                CurrentHealth -= hurtAmount;
                if (CurrentHealth <= 0)
                {
                    AudioController.Instance.Play(_gameOverSound);
                    EventHub.OnGameRestarted?.Invoke();
                }
                else
                {
                    AudioController.Instance.Play(_hurtSound);
                }

                StartCoroutine(Invulnerable());
            }
        }

        private IEnumerator Invulnerable()
        {
            _isInvulnerable = true;
            yield return new WaitForSeconds(_invulnerableTime);
            _isInvulnerable = false;
        }
    }
}