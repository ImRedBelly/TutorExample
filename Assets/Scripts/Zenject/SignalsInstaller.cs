using Zenject.Signals;

namespace Zenject
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<CancelTutorTokenSignal>();
            Container.DeclareSignal<CloseTextDialog>();
        }
    }
}