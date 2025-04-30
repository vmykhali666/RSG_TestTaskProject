using System;
using System.Collections.Generic;
using System.Linq;
using Content.Features.DamageablesModule.Scripts.Signals;
using Content.Features.StorageModule.Scripts;
using Core.InputModule;
using UnityEngine;
using Zenject;

namespace Content.Features.InteractionModule
{
    public class HealPlayerSystem : IInitializable, IDisposable
    {
        private readonly IInputListener _inputListener;
        private readonly SignalBus _signalBus;
        private readonly PlayerStorageService _storageService;
        private IStorage _storage;

        private List<IHealable> _items => _storage.GetAllItems<IHealable>();

        public HealPlayerSystem(IInputListener inputListener, SignalBus signalBus, PlayerStorageService storageService)
        {
            _inputListener = inputListener;
            _signalBus = signalBus;
            _storageService = storageService;
        }

        public void Initialize()
        {
            _storage = _storageService.Storage;
            _inputListener.OnHealPerformed += HandleHeal;
        }

        public void Dispose()
        {
            _inputListener.OnHealPerformed -= HandleHeal;
        }

        private void HandleHeal()
        {
            var item = _items.FirstOrDefault();
            if (item != null)
            {
                _signalBus.Fire(new HealPlayerSignal(item));
                _storage.RemoveItem((Item)item);
            }
            else
            {
                // Handle the case when there are no items to heal
                Debug.Log("No items available for healing.");
            }
        }
    }
}