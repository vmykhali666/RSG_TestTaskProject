using System;

namespace Content.Features.PlayerData.Scripts
{
    [Serializable]
    public class PlayerPersistentData : IDataModel
    {
        public float MaxHealth;
        public float CurrentHealth;
        public float Currency;
    }
}