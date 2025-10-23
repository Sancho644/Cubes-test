using System.Collections.Generic;
using System.Linq;
using Config;
using Core.Cubes.Config;
using Data;
using UnityEngine;
using Zenject;

namespace Core.Cubes.Services
{
    public class CubesService
    {
        [Inject] private readonly ConfigData _configData;
        [Inject] private readonly PlayerDataContainer _playerDataContainer;

        private CubeTowerController _towerController;

        public List<CubeType> GetCubesForSpawn()
        {
            var cubesShowCount = _configData.cubesConfig.cubesSpawnCount;
            var cubeConfigList = _configData.cubesConfig.cubeConfigsList.Select(x => x.cubeType).ToList();
            var cubesForSpawn = new List<CubeType>();
            for (int i = 0; i < cubesShowCount; i++)
            {
                if (i >= cubeConfigList.Count)
                {
                    break;
                }

                var cubeType = cubeConfigList[i];
                cubesForSpawn.Add(cubeType);
            }

            return cubesForSpawn;
        }

        public void RegisterCubeTower(CubeTowerController towerController)
        {
            _towerController = towerController;
        }

        public bool CubeCanPlaceAtTower(Vector2 screenPos)
        {
            if (_towerController == null)
            {
                return false;
            }

            return _towerController.CanPlaceAtTower(screenPos);
        }

        public void TryPlaceAtTowerScreen(CubeType cubeType, Vector2 screenPos)
        {
            if (_towerController == null)
            {
                return;
            }

            if (!RectTransformUtility.RectangleContainsScreenPoint(_towerController.TowerAreaRect, screenPos,
                    _towerController.Canvas.worldCamera))
            {
                return;
            }

            _towerController.PlaceCube(cubeType, screenPos);
        }

        public float GetCubeSpawnHorizontalOffset()
        {
            return _configData.cubesConfig.spawnOffsetPercent;
        }
    }
}