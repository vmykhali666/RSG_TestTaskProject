using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity.Datas
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
    }
}