using Localization.Services;
using TMPro;
using UI.Settings;
using UI.Tweens;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ActionCommentary : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private FadeTween fadeTween;

        [Inject] private readonly CubeActionSettings _actionSettings;
        [Inject] private readonly LanguageService _languageService;

        public void Setup(CubeActionType actionType)
        {
            var actionTextId = _actionSettings.GetActionText(actionType);
            var localizedText = _languageService.GetLocalizedText(actionTextId);
            text.text = localizedText;

            fadeTween.StartAnimation(() => { Destroy(gameObject); });
        }
    }
}