using UnityEngine;

namespace Content.Features.AIModule.Scripts.Entity
{
    [CreateAssetMenu(menuName = "Configurations/Inventory/" + nameof(StorageSettings),
        fileName = nameof(StorageSettings) + "_Default", order = 0)]
    public class StorageSettings : ScriptableObject
    {
        [Range(0, 100)]
        [SerializeField] private int _maxCapacity = 20;
        [Range(9, 18)]
        [SerializeField] private int _maxItemsQuantity = 18;
        public int MaxCapacity => _maxCapacity;
        
        public int MaxItemsQuantity => _maxItemsQuantity;
    }
}