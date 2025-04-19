using System.Collections.Generic;
using Content.Features.LootModule.Scripts;

namespace Content.Features.StorageModule.Scripts.Constraints
{
    public interface IStorageConstraintService
    {
        StorageConstraintResult CheckConstraints(List<Item> items, IStorage storage);
        
        StorageConstraintResult CheckConstraints(Loot loot, IStorage storage);
        
        StorageConstraintResult CheckConstraints(Item item, IStorage storage);
    }
}