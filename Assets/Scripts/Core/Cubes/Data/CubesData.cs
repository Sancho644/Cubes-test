using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cubes.Config;
using UnityEngine;

namespace Core.Cubes.Data
{
    [Serializable]
    public class CubesData
    {
        public List<ConcreteCubeData> cubeDataList = new();

        public void CreateCubeData(CubeType cubeType, Vector2 position, string id)
        {
            var cubeData = new ConcreteCubeData()
            {
                cubeType = cubeType,
                posX = position.x,
                posY = position.y,
                id = id
            };

            cubeDataList.Add(cubeData);
        }

        public void RemoveCubeData(string id)
        {
            var cubeData = GetCubeData(id);
            if (cubeData == null)
            {
                return;
            }

            cubeDataList.Remove(cubeData);
        }

        public void RewriteCubePosition(string id, RectTransform rect)
        {
            var cubeData = GetCubeData(id);
            if (cubeData == null)
            {
                return;
            }
            
            var anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);

            cubeData.posX = anchoredPosition.x;
            cubeData.posY = anchoredPosition.y;
        }

        private ConcreteCubeData GetCubeData(string id)
        {
            return cubeDataList.FirstOrDefault(x => x.id == id);
        }
    }
}