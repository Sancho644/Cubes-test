using Core.Cubes;
using Core.Cubes.Services;
using Extensions;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CubesScrollPanel : MonoBehaviour
    {
        [SerializeField] private Cube cubePrefab;
        [SerializeField] private RectTransform cubesRoot;

        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly CubesService _cubesService;

        private void Start()
        {
            RefreshCubes();
        }

        private void RefreshCubes()
        {
            var cubesForSpawn = _cubesService.GetCubesForSpawn();
            cubesRoot.ClearChildren();

            foreach (var cubeType in cubesForSpawn)
            {
                var cubeView = _instantiator.InstantiatePrefabForComponent<Cube>(cubePrefab, cubesRoot);
                cubeView.Setup(cubeType);
            }
        }
    }
}