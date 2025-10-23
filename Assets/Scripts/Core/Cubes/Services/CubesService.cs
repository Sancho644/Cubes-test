using System.Collections.Generic;
using System.Linq;
using Config;
using Core.Cubes.Config;
using Data;
using GameEvents;
using UI;
using UI.Events;
using UnityEngine;
using Zenject;

namespace Core.Cubes.Services
{
    public class CubesService
    {
        [Inject] private readonly ConfigData _configData;
        [Inject] private readonly PlayerDataContainer _playerDataContainer;
        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        private CubeTowerController _towerController;
        private CubeRemoveHoleController _removeHoleController;

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

        public void RegisterRemoveHole(CubeRemoveHoleController removeHoleController)
        {
            _removeHoleController = removeHoleController;
        }

        public bool CubeCanPlaceAtTower(Vector2 screenPos)
        {
            if (_towerController == null)
            {
                return false;
            }

            return _towerController.CanPlaceAtTower(screenPos);
        }

        public void TryPlaceAtTowerScreen(CubeType cubeType, Vector2 screenPos, Vector2 startDragPosition)
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

            var cubesCount = _towerController.GetCubesCount();
            if (cubesCount == 0)
            {
                _towerController.PlaceFirstCube(cubeType, screenPos);
                _gameEventsDispatcher.Dispatch(new CubeActionEvent(CubeActionType.Place));
            }
            else
            {
                _towerController.PlaceNextCube(cubeType, startDragPosition);
                _gameEventsDispatcher.Dispatch(new CubeActionEvent(CubeActionType.PlaceAtTower));
            }
        }

        public float GetCubeSpawnHorizontalOffset()
        {
            return _configData.cubesConfig.spawnOffsetPercent;
        }

        public bool CubeInsideRemoveHole()
        {
            if (_removeHoleController == null)
            {
                return false;
            }

            return _removeHoleController.IsInsideHole();
        }

        public void RemoveCubeFromTower(Cube cube)
        {
            if (_towerController == null)
            {
                return;
            }

            _towerController.RemoveCube(cube);
            _gameEventsDispatcher.Dispatch(new CubeActionEvent(CubeActionType.Destroy));
        }
    }
}