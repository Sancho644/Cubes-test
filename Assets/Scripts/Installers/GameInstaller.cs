using Core.Cubes;
using Core.Cubes.Services;
using Core.Cubes.Settings;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CubesSettings cubesSettings;
        [SerializeField] private CubesFactory cubesFactory;

        public override void InstallBindings()
        {
            Container.Bind<CubesService>().AsSingle();
            Container.BindInstance(cubesSettings).AsSingle();
            Container.BindInstance(cubesFactory).AsSingle();
        }
    }
}