namespace Core.GlobalSignalsModule.Scripts.Signals
{
    public class PlayerHealthChangedSignal 
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }

        public PlayerHealthChangedSignal(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}