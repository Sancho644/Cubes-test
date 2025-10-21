using Config;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private ConfigScriptableObject config;

        public override void InstallBindings()
        {
            Container.BindInstance(config.Data).AsSingle();
            Container.BindInstance(config).AsSingle();
        }
    }
}