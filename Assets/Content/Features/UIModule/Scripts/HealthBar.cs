using Content.Features.CameraModule;
using Content.Features.DamageablesModule.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _lerpSpeed = 10f;
        [SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);
        private PlayerCameraModel _cameraModel;
        private IDamageable _damageable;

        public IDamageable Damageable => _damageable;

        [Inject]
        private void InjectDependencies(PlayerCameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        public void Initialize(IDamageable damageable)
        {
            _damageable = damageable;
            _slider.maxValue = damageable.MaxHealth;
            _slider.value = damageable.CurrentHealth;
            _damageable.OnDamaged += OnDamaged;
            _damageable.OnHealthChanged += UpdateHealthBar;
            TrackPosition();
        }

        private void UpdateHealthBar(float current, float max)
        {
            UpdateHealthBar(current);
        }

        private void OnDamaged(float incomeDamage)
        {
            var currentHealth = _damageable.CurrentHealth;
            UpdateHealthBar(currentHealth);
        }

        private void LateUpdate()
        {
            TrackPosition();
        }

        private void TrackPosition()
        {
            var screenPos = _cameraModel.CurrentCamera.WorldToScreenPoint(_damageable.Position + _offset);
            screenPos.z = 0;
            UpdateScreenPosition(screenPos);
        }

        private void UpdateScreenPosition(Vector3 screenPos)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                screenPos,
                Time.deltaTime * _lerpSpeed
            );
        }

        private void UpdateHealthBar(float newHealth)
        {
            _slider.value = newHealth;
        }

        public void Destroy()
        {
            _damageable.OnDamaged -= OnDamaged;
            Destroy(gameObject);
        }
    }
}