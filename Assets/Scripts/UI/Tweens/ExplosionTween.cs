using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Tweens
{
    public class ExplosionTween : MonoBehaviour
    {
        [SerializeField] private float posYJumpOffset = 80f;
        [SerializeField] private float posYJumpDuration = 0.25f;
        [SerializeField] private float increaseScale = 1.4f;
        [SerializeField] private float increaseScaleDuration = 0.25f;
        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private float rotationDuration = 0.3f;
        [SerializeField] private Image image;
        [SerializeField] private Ease ease = Ease.OutQuad;
        [SerializeField] private RotateMode rotateMode = RotateMode.FastBeyond360;

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
                .Append(targetRect.DOAnchorPosY(targetRect.anchoredPosition.y + posYJumpOffset, posYJumpDuration).SetEase(ease))
                .Join(targetRect.DOScale(increaseScale, increaseScaleDuration).SetEase(ease))
                .Join(image.DOFade(0f, fadeDuration))
                .Join(targetRect.DORotate(new Vector3(0f, 0f, randomAngle), rotationDuration,rotateMode))
                .OnComplete(() => { onComplete?.Invoke(); });
        }
    }
}