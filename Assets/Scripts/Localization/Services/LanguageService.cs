using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Localization.Config;
using UnityEngine;
using Zenject;

namespace Localization.Services
{
    public class LanguageService
    {
        [Inject] private readonly PlayerDataContainer _playerDataContainer;
        [Inject] private readonly LocalizationConfig _localizationConfig;

        private readonly Dictionary<string, LocalizedPhraseConfig> _cachedPhrases = new();

        public LocalizationType GetLocalization()
        {
            return _playerDataContainer.Data.languageData.localizationType;
        }

        public string GetLocalizedText(string localizationKey)
        {
            if (localizationKey == string.Empty)
                throw new Exception("Localization key is empty");

            var localizationType = GetLocalization();
            var localizedText = GetLocalizedTextFromLocalization(localizationKey, localizationType);
            return localizedText;
        }

        private string GetLocalizedTextFromLocalization(string stringKey, LocalizationType localizationType)
        {
            if (!_cachedPhrases.TryGetValue(stringKey, out var phrase))
            {
                phrase = _localizationConfig.languages.localizedPhrases
                    .FirstOrDefault(x => x.localizationKey == stringKey);
                if (phrase == null)
                {
                    Debug.LogWarning($"There is no key {stringKey} for the phrase in the localization.");
                    return stringKey;
                }

                _cachedPhrases.Add(stringKey, phrase);
            }

            var localizedText = phrase.GetLocalized(localizationType);
            return string.IsNullOrEmpty(localizedText) ? stringKey : localizedText;
        }
    }
}