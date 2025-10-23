using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tweens
{
    public class FadeTween : MonoBehaviour
    {
        [SerializeField] private float endValue = 0f;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Image image;

        private Sequence _sequence;

        private void OnDestroy()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
        }

        public void StartAnimation(Action onComplete = null)
        {
            if (_sequence != null)
                return;

            _sequence = DOTween.Sequence()
                .Join(image.DOFade(endValue, duration))
                .OnComplete(() => { onComplete?.Invoke(); });
        }
    }
}