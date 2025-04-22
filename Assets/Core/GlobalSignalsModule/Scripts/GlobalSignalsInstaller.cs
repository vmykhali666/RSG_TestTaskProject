using Core.GlobalSignalsModule.Scripts.Signals;
using Zenject;

namespace Core.GlobalSignalsModule.Scripts
{
    public class GlobalSignalsInstaller : Installer<GlobalSignalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<DamagebleCreated>().OptionalSubscriber();

            Container.DeclareSignal<DamageableDestroyed>().OptionalSubscriber();
            
            Container.DeclareSignal<PlayerHealthChangedSignal>().OptionalSubscriber();
            
            Container.DeclareSignal<ReceivedCurrencySignal>().OptionalSubscriber();
            
            Container.DeclareSignal<SpentCurrencySignal>().OptionalSubscriber();
        }
    }
}