using System.Collections.Generic;
using System.Linq;
using Content.Features.LootModule.Scripts;

namespace Content.Features.StorageModule.Scripts.Constraints
{
    public class StorageConstraintService : IStorageConstraintService
    {
        private readonly List<IStorageConstraint> _constraints;
        private readonly IItemFactory _itemFactory;

        public StorageConstraintService(List<IStorageConstraint> constraints, IItemFactory itemFactory)
        {
            _constraints = constraints;
            _itemFactory = itemFactory;
        }

        public StorageConstraintResult CheckConstraints(List<Item> items, IStorage storage)
        {
            foreach (var constraint in _constraints)
            {
                var result = constraint.CheckItems(items, storage);
                if (result.IsValid is false)
                    return result;
            }

            return new StorageConstraintResult(true);
        }
        
        public StorageConstraintResult CheckConstraints(Loot loot, IStorage storage)
        {
            var itemTypes = loot.GetItemsInLoot();
            var items = itemTypes.Select(type => _itemFactory.GetItem(type)).ToList();
            return CheckConstraints(items, storage);
        }
    }
}