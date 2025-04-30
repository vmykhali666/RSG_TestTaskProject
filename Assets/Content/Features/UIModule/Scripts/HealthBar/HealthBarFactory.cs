using System;
using Content.Features.CameraModule;
using Content.Features.DamageablesModule.Scripts;
using Content.Features.PrefabSpawner;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.UIModule.Scripts.HealthBar
{
    public class HealthBarFactory : IHealthBarFactory
    {
        private readonly IPrefabsFactory _prefabsFactory;
        private readonly UIRootReferences _uiRoot;
        private readonly PlayerCameraModel _cameraModel;
        private readonly DiContainer _diContainer;

        public HealthBarFactory(IPrefabsFactory prefabsFactory, UIRootReferences uiRootReferences,
            PlayerCameraModel cameraModel, DiContainer container)
        {
            _prefabsFactory = prefabsFactory;
            _uiRoot = uiRootReferences;
            _cameraModel = cameraModel;
            _diContainer = container;
        }

        public HealthBarPresenter Create(IDamageable damageable)
        {
            var screenPos = _cameraModel.CurrentCamera.WorldToScreenPoint(damageable.Position);

            var healthBarView = _prefabsFactory
                .Create<HealthBarView>(Address.UI.HealthBar, screenPos, _uiRoot.HealthBarsParent);
            
            healthBarView.gameObject.SetActive(false);

            var presenter = _diContainer.Instantiate<HealthBarPresenter>(
                new object[] { healthBarView, _cameraModel, damageable });
            
            return presenter;
        }
    }
}