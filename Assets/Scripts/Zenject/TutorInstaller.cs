using Tutor.GameTutorials;

namespace Zenject
{
    public class TutorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UITutorial>().AsSingle();
        }
    }
}