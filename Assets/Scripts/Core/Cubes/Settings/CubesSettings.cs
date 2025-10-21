using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cubes.Config;
using UnityEngine;

namespace Core.Cubes.Settings
{
    [CreateAssetMenu(fileName = "CubesSettings", menuName = "ScriptableObjects/CubesSettingsScriptableObject",
        order = 1)]
    public class CubesSettings : ScriptableObject
    {
        [Serializable]
        private struct ConcreteCubeSettings
        {
            public CubeType cubeType;
            public Sprite cubeSprite;
        }

        [SerializeField] private List<ConcreteCubeSettings> cubeSettings;

        public Sprite GetCubeSprite(CubeType cubeType)
        {
            return GetCubeSettings(cubeType).cubeSprite;
        }

        private ConcreteCubeSettings GetCubeSettings(CubeType cubeType)
        {
            return cubeSettings.FirstOrDefault(x => x.cubeType == cubeType);
        }
    }
}