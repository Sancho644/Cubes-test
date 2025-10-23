using System;
using Core.Cubes.Config;

namespace Core.Cubes.Data
{
    [Serializable]
    public class ConcreteCubeData
    {
        public CubeType cubeType;
        public float posX;
        public float posY;
        public string id;
    }
}