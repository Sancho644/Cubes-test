using Core.Cubes.Config;
using Core.Cubes.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Cubes
{
    public abstract class AbstractCube : MonoBehaviour
    {
        [SerializeField] private Image cubeBackground;
        
        [Inject] private readonly CubesSettings _cubesSettings;

        protected CubeType CubeType { get; private set; }
        
        public void Setup(CubeType cubeType)
        {
            CubeType = cubeType;

            Refresh();
        }
        
        private void Refresh()
        {
            var cubeSprite = _cubesSettings.GetCubeSprite(CubeType);
            cubeBackground.sprite = cubeSprite;
        }
    }
}