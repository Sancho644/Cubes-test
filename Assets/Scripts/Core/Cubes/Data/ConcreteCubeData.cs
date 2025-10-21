using System;
using Core.Cubes.Config;
using UnityEngine;

namespace Core.Cubes.Data
{
    [Serializable]
    public class ConcreteCubeData
    {
        public CubeType cubeType;
        public Vector3 spawnPosition;
    }
}