using System.Collections.Generic;
using System.Linq;

namespace Content.Features.StorageModule.Scripts.Constraints
{
    public class CapacityStorageConstraint : IStorageConstraint
    {
        public StorageConstraintResult CheckItems(List<Item> items, IStorage storage)
        {
            var totalWeight = items.Sum(item => item.Weight);
            var isValid = storage.GetCurrentCapacity() + totalWeight <= storage.GetMaxCapacity();
            return new StorageConstraintResult(isValid, isValid ? string.Empty : "Not enough capacity");
        }
    }
}