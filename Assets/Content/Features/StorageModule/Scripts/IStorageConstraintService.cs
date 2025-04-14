using System.Collections.Generic;
using Content.Features.LootModule.Scripts;

namespace Content.Features.StorageModule.Scripts
{
    public interface IStorageConstraintService
    {
        StorageConstraintResult CheckConstraints(List<Item> items, IStorage storage);
        
        StorageConstraintResult CheckConstraints(Loot loot, IStorage storage);
    }
}