namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class SpentCurrencySignal
    {
        public float CurrencyAmount { get; }
        
        public SpentCurrencySignal(float currencyAmount)
        {
            CurrencyAmount = currencyAmount;
        }
    }
}