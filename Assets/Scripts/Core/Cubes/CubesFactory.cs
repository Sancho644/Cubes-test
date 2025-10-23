using System;
using Core.Cubes.Config;
using UnityEngine;
using Zenject;

namespace Core.Cubes
{
    public class CubesFactory : MonoBehaviour
    {
        [SerializeField] private Cube cubePrefab;

        [Inject] private readonly IInstantiator _instantiator;

        public Cube CreateCube(CubeType cubeType, Transform root)
        {
            var cube = _instantiator.InstantiatePrefabForComponent<Cube>(cubePrefab, root);
            cube.Setup(cubeType);

            return cube;
        }

        public string CreateCubeId()
        {
            var cubeId = Guid.NewGuid().ToString();

            return cubeId;
        }
    }
}