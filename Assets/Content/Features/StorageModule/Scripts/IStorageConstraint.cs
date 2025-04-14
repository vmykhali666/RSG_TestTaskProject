using System.Collections.Generic;

namespace Content.Features.StorageModule.Scripts
{
    public interface IStorageConstraint
    {
        StorageConstraintResult CheckItems(List<Item> items, IStorage storage);
    }
}