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

        public void Setup(CubeActionType actionType)
        {
            var actionText = _actionSettings.GetActionText(actionType);
            text.text = actionText;

            fadeTween.StartAnimation(() => { Destroy(gameObject); });
        }
    }
}