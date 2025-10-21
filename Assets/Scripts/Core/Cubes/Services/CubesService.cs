using System.Collections.Generic;
using System.Linq;
using Config;
using Core.Cubes.Config;
using Zenject;

namespace Core.Cubes.Services
{
    public class CubesService
    {
        [Inject] private readonly ConfigData _configData;

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
    }
}