using System;
using Content.Features.CameraModule;
using Content.Features.DamageablesModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.UIModule.Scripts.HealthBar
{
    public class HealthBarPresenter : IInitializable, IDisposable, ILateTickable
    {
        private readonly HealthBarView _view;
        private readonly PlayerCameraModel _cameraModel;
        private readonly IDamageable _damageable;

        public IDamageable Damageable => _damageable;

        [Inject]
        public HealthBarPresenter(HealthBarView view, PlayerCameraModel cameraModel, IDamageable damageable)
        {
            _view = view;
            _cameraModel = cameraModel;
            _damageable = damageable;
        }

        public void Initialize()
        {
            _view.SetMaxHealth(_damageable.MaxHealth);
            _view.UpdateHealth(_damageable.CurrentHealth);

            _damageable.OnDamaged += OnDamaged;
            _damageable.OnHealthChanged += OnHealthChanged;

            UpdatePosition();
            _view.gameObject.SetActive(true);
        }

        private void OnDamaged(float damage)
        {
            _view.UpdateHealth(_damageable.CurrentHealth);
        }

        private void OnHealthChanged(float current, float max)
        {
            _view.UpdateHealth(current);
        }

        public void LateTick()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var screenPos = _cameraModel.CurrentCamera.WorldToScreenPoint(_damageable.Position + _view.Offset);
            screenPos.z = 0;
            _view.UpdatePosition(screenPos);
        }

        public void Dispose()
        {
            _damageable.OnDamaged -= OnDamaged;
            _damageable.OnHealthChanged -= OnHealthChanged;
            GameObject.Destroy(_view.gameObject);
        }
    }
}