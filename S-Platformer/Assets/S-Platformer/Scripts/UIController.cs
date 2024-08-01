using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SPlatformer
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private GameController _gameController;

        [SerializeField]
        private TextMeshProUGUI _keysCurrentText;

        [SerializeField]
        private TextMeshProUGUI _keysFullText;

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

        private void Start()
        {
            _keysFullText.text = _gameController.KeysAmount.ToString();
        }

        private void HealthChangedHandler(int health, int healthMax)
        {
             _healthSlider.value = ((float)health / healthMax);
        }

        private void KeyChangedHandler(int key, int keysMax)
        {
            _keysCurrentText.text = key.ToString();
        }
    }
}