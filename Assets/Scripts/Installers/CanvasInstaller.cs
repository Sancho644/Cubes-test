using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField] private CanvasSettings canvasSettings;
        [SerializeField] private Canvas canvas;

        public override void InstallBindings()
        {
            canvasSettings.SetCanvas(canvas);

            Container.BindInstance(canvasSettings).AsSingle();
        }
    }
}