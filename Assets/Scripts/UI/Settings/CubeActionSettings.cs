using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Settings
{
    [CreateAssetMenu(fileName = "CubeActionSettings", menuName = "ScriptableObjects/CubeActionSettingsScriptableObject",
        order = 1)]
    public class CubeActionSettings : ScriptableObject
    {
        [Serializable]
        private struct ConcreteCubeActionSettings
        {
            public CubeActionType actionType;
            public string actionText;
        }
        
        [SerializeField] private List<ConcreteCubeActionSettings> cubeActionSettings;

        public string GetActionText(CubeActionType actionType)
        {
            return GetCubeActionSettings(actionType).actionText;
        }

        private ConcreteCubeActionSettings GetCubeActionSettings(CubeActionType actionType)
        {
            return cubeActionSettings.First(x => x.actionType == actionType);
        }
    }
}