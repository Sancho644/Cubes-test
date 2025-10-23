using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Tweens
{
    public class ExplosionTween : MonoBehaviour
    {
        [SerializeField] private float increaseScale = 1.4f;
        [SerializeField] private float increaseScaleDuration = 0.25f;
        [SerializeField] private Image image;

        private Sequence _sequence;

        private void OnDestroy()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
        }

        public void StartAnimation(RectTransform targetRect, Action onComplete = null)
        {
            if (_sequence != null)
                return;

            var randomAngle = Random.Range(-180f, 180f);

            _sequence = DOTween.Sequence()
                .Append(targetRect.DOAnchorPosY(targetRect.anchoredPosition.y + 80f, 0.25f).SetEase(Ease.OutQuad))
                .Join(targetRect.DOScale(increaseScale, increaseScaleDuration).SetEase(Ease.OutQuad))
                .Join(image.DOFade(0f, 0.3f))
                .Join(targetRect.DORotate(new Vector3(0f, 0f, randomAngle), 0.3f, RotateMode.FastBeyond360))
                .OnComplete(() => { onComplete?.Invoke(); });
        }
    }
}