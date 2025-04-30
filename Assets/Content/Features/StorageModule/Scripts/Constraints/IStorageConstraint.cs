using System.Collections.Generic;

namespace Content.Features.StorageModule.Scripts.Constraints
{
    public interface IStorageConstraint
    {
        StorageConstraintResult CheckItems(List<Item> items, IStorage storage);
    }
}