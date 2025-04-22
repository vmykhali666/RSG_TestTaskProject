using System;
using System.Collections.Generic;
using Content.Features.DamageablesModule.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using Zenject;

namespace Content.Features.UIModule.Scripts.HealthBar
{
    public class HealthBarsManager : IInitializable, IDisposable, ILateTickable
    {
        private readonly IHealthBarFactory _healthBarFactory;
        private readonly SignalBus _signalBus;
        private List<HealthBarPresenter> _healthBarPresenters;

        public HealthBarsManager(IHealthBarFactory healthBarFactory, SignalBus signalBus)
        {
            _healthBarFactory = healthBarFactory;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _healthBarPresenters = new List<HealthBarPresenter>();
            _signalBus.Subscribe<DamagebleCreated>(OnPlayerCreated);
            _signalBus.Subscribe<DamageableDestroyed>(OnPlayerDestroyed);
        }

        private void OnPlayerDestroyed(DamageableDestroyed obj)
        {
            DestroyHealthBar(obj.Damageable);
        }

        private void OnPlayerCreated(DamagebleCreated obj)
        {
            CreateHealthBar(obj.Damageable);
        }

        public void Dispose()
        {
            if (_healthBarPresenters == null) return;

            foreach (var presenter in _healthBarPresenters)
            {
                presenter.Dispose();
            }

            _healthBarPresenters.Clear();
            _signalBus.Unsubscribe<DamagebleCreated>(OnPlayerCreated);
            _signalBus.Unsubscribe<DamageableDestroyed>(OnPlayerDestroyed);
        }

        private void CreateHealthBar(IDamageable damageable)
        {
            var presenter = _healthBarFactory.Create(damageable);
            _healthBarPresenters.Add(presenter);
            presenter.Initialize();
        }

        private void DestroyHealthBar(IDamageable damageable)
        {
            var presenter = _healthBarPresenters.Find(p => p.Damageable == damageable);
            if (presenter == null) return;
            presenter.Dispose();
            _healthBarPresenters.Remove(presenter);
        }

        public void LateTick()
        {
            foreach (var presenter in _healthBarPresenters)
            {
                presenter.LateTick();
            }
        }
    }
}