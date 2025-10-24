using System;
using Localization.Config;
using Localization.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Localization
{
    public class LocalizationInjector : MonoBehaviour
    {
        [SerializeField] private string localizationKey;

        [Inject] private readonly LanguageService _languageService;

        private TextMeshProUGUI _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (_textComponent == null)
            {
                throw new Exception("Text field not assigned");
            }

            var localizationType = _languageService.GetLocalization();
            if (localizationType == LocalizationType.None)
            {
                _textComponent.text = localizationKey;
                return;
            }

            var localizedText = _languageService.GetLocalizedText(localizationKey);
            _textComponent.text = string.IsNullOrEmpty(localizedText) ? localizationKey : localizedText;
        }
    }
}