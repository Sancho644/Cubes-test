using System;
using System.Collections.Generic;

namespace Core.Cubes.Config
{
    [Serializable]
    public class CubesConfig
    {
        public int cubesSpawnCount;
        public float spawnOffsetPercent;
        public List<ConcreteCubeConfig> cubeConfigsList;
    }
}