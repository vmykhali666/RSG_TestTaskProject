using Content.Features.StorageModule.Scripts;
using Zenject;

namespace Content.Features.AIModule.Scripts.Entity
{
    public class PlayerMonoEntity : MonoEntity
    {
        private PlayerStorageService _playerStorageService;

        [Inject]
        private void InjectDependencies(PlayerStorageService playerStorageService)
        {
            _playerStorageService = playerStorageService;
        }

        protected override IStorage CreateStorage()
        {
            return _playerStorageService.Storage;
        }
    }
}