using Core.Cubes.Config;
using Core.Cubes.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Cubes
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Image cubeBackground;

        [Inject] private readonly CubesSettings _cubesSettings;

        private CubeType _cubeType;
        
        public void Setup(CubeType cubeType)
        {
            _cubeType = cubeType;
            
            Refresh();
        }

        private void Refresh()
        {
            var cubeSprite = _cubesSettings.GetCubeSprite(_cubeType);
            cubeBackground.sprite = cubeSprite;
        }
    }
}