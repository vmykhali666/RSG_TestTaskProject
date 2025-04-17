using System;
using System.Collections.Generic;
using Content.Features.CameraModule;
using Content.Features.PrefabSpawner;
using Core.GlobalSignalsModule.Scripts.Signals;
using Global.Scripts.Generated;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class HealthBarInitializer : IInitializable, IDisposable
    {
        private readonly IPrefabsFactory _prefabsFactory;
        private readonly SignalBus _signalBus;
        private readonly UIRootReferences _uiRoot;
        private readonly PlayerCameraModel _playerCameraModel;
        public List<HealthBar> HealthBars { get; private set; }

        public HealthBarInitializer(IPrefabsFactory prefabsFactory, SignalBus signalBus,
            UIRootReferences uiRootReferences, PlayerCameraModel cameraModel)
        {
            _prefabsFactory = prefabsFactory;
            _signalBus = signalBus;
            _uiRoot = uiRootReferences;
            _playerCameraModel = cameraModel;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DamageableCreated>(OnCreateHealthBarSignal);
            _signalBus.Subscribe<DamageableDestroyed>(OnDestroyHealthBarSignal);
            HealthBars = new List<HealthBar>();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DamageableCreated>(OnCreateHealthBarSignal);
            _signalBus.Unsubscribe<DamageableDestroyed>(OnDestroyHealthBarSignal);
        }

        private void OnDestroyHealthBarSignal(DamageableDestroyed obj)
        {
            var healthBar = HealthBars.Find(x => x.Damageable == obj.Damageable);
            healthBar?.Destroy();
            HealthBars.Remove(healthBar);
        }

        private void OnCreateHealthBarSignal(DamageableCreated obj)
        {
            var damageable = obj.Damageable;

            var screenPos = _playerCameraModel.CurrentCamera.WorldToScreenPoint(damageable.Position);

            var healthBar = _prefabsFactory
                .Create(Address.UI.HealthBar, screenPos, _uiRoot.HealthBarsParent)
                .GetComponent<HealthBar>();

            healthBar.Initialize(damageable);

            HealthBars.Add(healthBar);
        }
    }
}