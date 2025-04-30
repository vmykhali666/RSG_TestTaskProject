using UnityEngine;

namespace Content.Features.PlayerData.Scripts.Datas
{
    [CreateAssetMenu(
        menuName = "Configurations/Entity/" + nameof(EntitiesConfiguration) + "/" + nameof(SellerEntityData),
        fileName = nameof(SellerEntityData), order = 0)]
    public class SellerEntityData : EntityData, IInteractableData
    {
        [field: SerializeField] public float InteractDistance { get; private set; }
    }
}