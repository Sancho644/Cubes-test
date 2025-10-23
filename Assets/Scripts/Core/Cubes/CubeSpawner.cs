using Core.Cubes.Services;
using UnityEngine;
using Zenject;

namespace Core.Cubes
{
    public class CubeSpawner : MonoBehaviour
    {
        [Inject] private readonly CubesService _cubesService;
        [Inject] private readonly CubesFactory _cubesFactory;

        private void Start()
        {
            TrySpawnCubes();
        }

        private void TrySpawnCubes()
        {
            var hasCubesData = _cubesService.HasCubesData();
            if (!hasCubesData)
            {
                return;
            }

            var cubesDataList = _cubesService.GetCubesDataList();
            foreach (var cubeData in cubesDataList)
            {
                var cube = _cubesFactory.CreateCube(cubeData.cubeType, transform);
                cube.SetId(cubeData.id);
                var cubeRect = cube.GetRect();
                cubeRect.anchoredPosition = new Vector2(cubeData.posX, cubeData.posY);
                _cubesService.SetStackedCube(cube);
            }
        }
    }
}