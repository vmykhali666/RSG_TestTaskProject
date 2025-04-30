using UnityEngine;

namespace Content.Features.PlayerData.Scripts.Datas
{
    [CreateAssetMenu(
        menuName = "Configurations/Entity/" + nameof(EntitiesConfiguration) + "/" + nameof(EnemyEntityData),
        fileName = nameof(EnemyEntityData), order = 0)]
    public class EnemyEntityData : EntityData, IDamageableData, IAttackableData, IInteractableData, IMovableData
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float CurrentHealth { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public float InteractDistance { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public bool IsAggressive { get; private set; } = true;
    }
}