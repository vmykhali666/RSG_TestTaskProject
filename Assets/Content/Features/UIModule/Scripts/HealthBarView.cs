using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.UIModule.Scripts
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void UpdateHealthBar(float value, int max)
        {
            if (_slider == null)
            {
                Debug.LogError("Slider is not assigned in the inspector.");
                return;
            }

            if (max <= 0)
            {
                Debug.LogError("Max health value must be greater than zero.");
                return;
            }

            _slider.value = Mathf.Clamp(value, 0, max);
            _slider.maxValue = max;
        }
    }
}