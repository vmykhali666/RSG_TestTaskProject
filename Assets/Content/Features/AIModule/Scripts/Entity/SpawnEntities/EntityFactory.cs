using Content.Features.PrefabSpawner;
using Global.Scripts.Generated;

namespace Content.Features.AIModule.Scripts.Entity.SpawnEntities
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IPrefabsFactory _prefabsFactory;
        private readonly PlayerEntityModel _playerEntityModel;

        public EntityFactory(IPrefabsFactory prefabFactory, PlayerEntityModel playerEntityModel)
        {
            _playerEntityModel = playerEntityModel;
            _prefabsFactory = prefabFactory;
        }

        public IEntity CreateEntity(EntityMarker spawnMarker)
        {
            switch (spawnMarker.SpawnType)
            {
                case EntityType.Player:
                    var player = _prefabsFactory.Create(Address.Prefabs.Player, spawnMarker.SpawnPoint.position)
                        .GetComponent<IEntity>();
                    _playerEntityModel.PlayerEntity = player;
                    _prefabsFactory.Create(Address.Prefabs.PlayerCamera);
                    return player;
                case EntityType.Enemy:
                    var enemy = _prefabsFactory.Create(Address.Prefabs.Standard_Enemy, spawnMarker.SpawnPoint.position)
                        .GetComponent<IEntity>();
                    return enemy;
                case EntityType.Seller:
                    var seller = _prefabsFactory.Create(Address.Prefabs.Seller, spawnMarker.SpawnPoint.position)
                        .GetComponent<IEntity>();
                    return seller;
                default:
                    goto case EntityType.Enemy;
            }
        }
    }
}