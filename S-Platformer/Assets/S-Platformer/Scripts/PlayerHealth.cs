using UnityEngine;

namespace SPlatformer
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private PickableEventChannel _healthChannel;

        [SerializeField]
        private int _maxHealth;

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
            _healthChannel.OnPick += PickHandler;
            EventHub.OnFallingHappened += Hurt;
            EventHub.OnGameRestarted += Start;
        }

        private void OnDestroy()
        {
            EventHub.OnGameRestarted -= Start;
            EventHub.OnFallingHappened -= Hurt;
            _healthChannel.OnPick -= PickHandler;
        }

        private void PickHandler(Pickable pickable)
        {
            pickable.gameObject.SetActive(!Heal(((MedicalKit)pickable).Increment));
        }

        private void Start()
        {
            CurrentHealth = _maxHealth;
        }

        public bool Heal(int healAmount)
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
            CurrentHealth -= hurtAmount;
            if (CurrentHealth <= 0)
            {
                EventHub.OnGameRestarted?.Invoke();
            }
        }
    }
}