using Localization.Config;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(fileName = "Localization", menuName = "ScriptableObjects/LocalizationScriptableObject", order = 1)]
    public class LocalizationScriptableObject : ScriptableObject
    {
        [SerializeField] private LocalizationConfig localizationConfig;

        public LocalizationConfig LocalizationConfig => localizationConfig;
    }
}