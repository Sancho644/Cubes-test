using System;
using Core.Cubes.Data;
using Localization.Data;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public CubesData cubesData = new();
        public LanguageData languageData = new();
    }
}