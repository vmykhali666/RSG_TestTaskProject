using Zenject;

namespace Content.Features.ShopModule.Scripts
{
    public class ShopModuleInstaller : Installer<ShopModuleInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CurrencyPaymentService>().AsSingle();
        }
    }
}