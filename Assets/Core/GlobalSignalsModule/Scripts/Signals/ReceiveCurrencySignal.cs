using Content.Features.PlayerData.Scripts;

namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class ReceiveCurrencySignal
    {
        public PlayerPersistentData PlayerPersistentData { get; }
        
        public ReceiveCurrencySignal(PlayerPersistentData playerPersistentData)
        {
            PlayerPersistentData = playerPersistentData;
        }
    }
}