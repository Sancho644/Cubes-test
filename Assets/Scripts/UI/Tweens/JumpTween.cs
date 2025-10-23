using DG.Tweening;
using UnityEngine;

namespace UI.Tweens
{
    public class JumpTween : MonoBehaviour
    {
        [SerializeField] private float jumpDuration = 0.5f;
        [SerializeField] private float landingDuration = 0.5f;
        [SerializeField] private float bounceHeight = 50f;
        [SerializeField] private Ease ease;

        private Sequence _sequence;

        private void OnDestroy()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
        }

        public void StartAnimation(RectTransform targetRect, Vector2 endPosition)
        {
            if (_sequence != null)
                return;

            _sequence = DOTween.Sequence()
                .Append(targetRect.DOAnchorPos(endPosition + new Vector2(0f, bounceHeight), jumpDuration))
                .SetEase(ease)
                .Append(targetRect.DOAnchorPos(endPosition, landingDuration))
                .OnComplete(() => targetRect.anchoredPosition = endPosition);
        }
    }
}