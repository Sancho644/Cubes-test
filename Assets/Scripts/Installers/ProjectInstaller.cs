using GameManager;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameLifecycleController gameController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameLifecycleController>()
                .FromInstance(gameController).AsSingle();
        }
    }
}
