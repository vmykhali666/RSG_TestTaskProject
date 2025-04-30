using Content.Features.PlayerData.Scripts;
using Core.GlobalSignalsModule.Scripts.Signals;
using Zenject;

namespace Content.Features.ShopModule.Scripts
{
    public class CurrencyPaymentService : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly BasePersistor<PlayerPersistentData> _playerPersistor;

        public CurrencyPaymentService(SignalBus signalBus, BasePersistor<PlayerPersistentData> playerPersistor)
        {
            _signalBus = signalBus;
            _playerPersistor = playerPersistor;
        }

        public void Initialize()
        {
        }

        public void AddCurrency(float amount)
        {
            var dataModel = _playerPersistor.GetDataModel();
            dataModel.Currency += amount;
            _playerPersistor.UpdateModel(dataModel);
            _playerPersistor.SaveData();
            _signalBus.Fire(new ReceivedCurrencySignal(dataModel.Currency));
        }

        public void RemoveCurrency(float amount)
        {
            var dataModel = _playerPersistor.GetDataModel();
            dataModel.Currency -= amount;
            _playerPersistor.UpdateModel(dataModel);
            _playerPersistor.SaveData();
            _signalBus.Fire(new SpentCurrencySignal(dataModel.Currency));
        }

        public bool CanPay(float amount)
        {
            var dataModel = _playerPersistor.GetDataModel();
            return dataModel.Currency >= amount;
        }
    }
}