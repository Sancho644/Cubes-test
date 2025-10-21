﻿using Data;
using Zenject;

namespace Installers
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerDataContainer>().AsSingle();
        }
    }
}