using Localization.Services;
using Zenject;

namespace Installers
{
    public class LocalizationDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LanguageService>().AsSingle();
        }
    }
}