using System.Collections.Generic;
using Content.Features.StorageModule.Scripts;
using UnityEngine;
using Zenject;

namespace Content.Features.LootModule.Scripts {
    public class LootSpawner : MonoBehaviour {
        [SerializeField] private List<Loot> _lootToSpawn;
        private DiContainer _diContainer;
        private IItemFactory _itemFactory;

        [Inject]
        public void InjectDependencies(DiContainer diContainer, IItemFactory itemFactory)
        {
            _diContainer = diContainer;
            _itemFactory = itemFactory;
        }

        public void SpawnLoot() {
            foreach (Loot loot in _lootToSpawn)
            {
                var types = loot.GetItemsInLoot();
                
                foreach (var type in types)
                {
                    var item = _itemFactory.GetItem(type);
                    var roll = Random.value;
                    if (item is IDroppable drop && roll < drop.DropChance)
                    {
                        _diContainer.InstantiatePrefab(loot.gameObject, transform.position, Quaternion.identity, null);
                    }
                }
            }
        }
    }
}