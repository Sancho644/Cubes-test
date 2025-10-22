using GameEvents;
using Zenject;

namespace Installers
{
    public class GameEventsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameEventsDispatcher>().AsSingle();
        }
    }
}