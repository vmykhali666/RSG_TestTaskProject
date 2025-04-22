using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.UIModule.Scripts.HealthBar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _lerpSpeed = 10f;
        [SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);

        public void SetMaxHealth(float maxHealth)
        {
            _slider.maxValue = maxHealth;
        }

        public void UpdateHealth(float currentHealth)
        {
            _slider.value = currentHealth;
        }

        public void UpdatePosition(Vector3 screenPosition)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                screenPosition,
                Time.deltaTime * _lerpSpeed
            );
        }

        public Vector3 Offset => _offset;
    }
}