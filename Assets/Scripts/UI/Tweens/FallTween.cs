using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Tweens
{
    public class FallTween : MonoBehaviour
    {
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private Ease ease;

        private Sequence _sequence;

        private void OnDestroy()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
        }

        public void StartAnimation(RectTransform targetRect, float height, Action onComplete = null)
        {
            _sequence = DOTween.Sequence()
                .Append(targetRect.DOAnchorPosY(targetRect.anchoredPosition.y - height, duration))
                .SetEase(ease)
                .OnComplete(() => { onComplete?.Invoke(); });
        }
    }
}