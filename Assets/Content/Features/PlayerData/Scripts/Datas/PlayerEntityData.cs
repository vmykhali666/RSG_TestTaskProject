using UnityEngine;

namespace Content.Features.PlayerData.Scripts.Datas
{
    [CreateAssetMenu(
        menuName = "Configurations/Entity/" + nameof(EntitiesConfiguration) + "/" + nameof(PlayerEntityData),
        fileName = nameof(PlayerEntityData), order = 0)]
    public class PlayerEntityData : EntityData, IDamageableData, IAttackableData, IInteractableData, IMovableData
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float CurrentHealth { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public float InteractDistance { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        /// <summary>
        /// Explicit conversion from PlayerEntityData to PlayerPersistentData. To Save data in peristor. For Default values.
        /// </summary>
        /// <param name="playerEntityData"></param>
        /// <returns></returns>
        public static implicit operator PlayerPersistentData(PlayerEntityData playerEntityData)
        {
            return new PlayerPersistentData
            {
                MaxHealth = playerEntityData.MaxHealth,
                CurrentHealth = playerEntityData.CurrentHealth,
                Currency = 0,
            };
        }
    }
}