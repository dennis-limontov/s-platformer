using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SPlatformer
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _keysText;

        [SerializeField]
        private Slider _healthSlider;

        private void Awake()
        {
            EventHub.OnHealthChanged += HealthChangedHandler;
            EventHub.OnKeyChanged += KeyChangedHandler;
        }

        private void OnDestroy()
        {
            EventHub.OnKeyChanged -= KeyChangedHandler;
            EventHub.OnHealthChanged -= HealthChangedHandler;
        }

        private void HealthChangedHandler(int health, int healthMax)
        {
             _healthSlider.value = ((float)health / healthMax);
        }

        private void KeyChangedHandler(int key, int keysMax)
        {
            _keysText.text = $"собрано {key}/{keysMax} ключей";
        }
    }
}