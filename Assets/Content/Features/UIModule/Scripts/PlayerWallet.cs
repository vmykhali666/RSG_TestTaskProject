using System;
using Content.Features.PlayerData.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class PlayerWallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;
        private SignalBus _signalBus;
        private BasePersistor<PlayerPersistentData> _playerPersistentData;
        private string _currencyText = "coins:";


        [Inject]
        private void InjectDependencies(SignalBus signalBus, BasePersistor<PlayerPersistentData> persistor)
        {
            _signalBus = signalBus;
            _playerPersistentData = persistor;
        }

        private void Awake()
        {
            var playerData = _playerPersistentData.GetDataModel();
            _signalBus.Subscribe<ReceiveCurrencySignal>(OnReceiveCurrency);
            UpdateWallet(playerData);
        }

        private void OnReceiveCurrency()
        {
            UpdateWallet(_playerPersistentData.GetDataModel());
        }

        private void UpdateWallet(PlayerPersistentData playerData)
        {
            _text.text = _currencyText + " " + playerData.Currency;
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<ReceiveCurrencySignal>(OnReceiveCurrency);
        }
    }
}