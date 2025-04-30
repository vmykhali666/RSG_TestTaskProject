namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class ReceivedCurrencySignal
    {
        public float CurrencyAmount { get; }
        
        public ReceivedCurrencySignal(float currencyAmount)
        {
            CurrencyAmount = currencyAmount;
        }
    }
}