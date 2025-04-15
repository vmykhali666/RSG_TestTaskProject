using UnityEngine;

namespace Content.Features.StorageModule.Scripts
{
    [CreateAssetMenu(menuName = "Configurations/Inventory/" + nameof(StorageSettings),
        fileName = nameof(StorageSettings) + "_Default", order = 0)]
    public class StorageSettings : ScriptableObject
    {
        [SerializeField] private float _maxCapacity;
        public float MaxCapacity => _maxCapacity;
    }
}