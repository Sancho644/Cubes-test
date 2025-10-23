using System.Collections.Generic;
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

        private readonly List<Cube> _cubesList = new();

        private void Start()
        {
            RefreshCubes();
        }

        public void SetCubesRaycastEnabled(bool enable)
        {
            foreach (var cube in _cubesList)
            {
                cube.EnableRaycasts(enable);
            }
        }

        private void RefreshCubes()
        {
            var cubesForSpawn = _cubesService.GetCubesForSpawn();
            cubesRoot.ClearChildren();

            foreach (var cubeType in cubesForSpawn)
            {
                var cube = _instantiator.InstantiatePrefabForComponent<Cube>(cubePrefab, cubesRoot);
                cube.Setup(cubeType);
                _cubesList.Add(cube);
            }
        }
    }
}