using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Localization.Config
{
    [Serializable]
    public class LocalizedPhraseConfig
    {
        public string localizationKey;
        public List<LocalizedTextConfig> localizedTexts = new();

        public string GetLocalized(LocalizationType localizationType)
        {
            var localizedText = localizedTexts.FirstOrDefault(x => x.localizationType == localizationType);
            if (localizedText == null)
            {
                Debug.LogWarning($"Could not find localization with type: {localizationType}, key: {localizationKey} ");
                return string.Empty;
            }

            return localizedText.text;
        }
    }
}