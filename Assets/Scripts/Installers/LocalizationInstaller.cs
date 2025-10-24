using Localization;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationScriptableObject localization;

        public override void InstallBindings()
        {
            Container.BindInstance(localization.LocalizationConfig).AsSingle();
            Container.BindInstance(localization).AsSingle();
        }
    }
}