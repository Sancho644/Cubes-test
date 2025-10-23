using Core.Cubes.Services;
using GameEvents;
using UI;
using UI.Events;
using UI.Tweens;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Cubes
{
    public class Cube : AbstractCube, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animations")] [SerializeField]
        private JumpTween jumpTween;

        [SerializeField] private FallTween fallTween;
        [SerializeField] private ExplosionTween explosionTween;
        [SerializeField] private FadeTween fadeTween;

        [Inject] private readonly CubesFactory _cubesFactory;
        [Inject] private readonly CubesService _cubesService;
        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;

        public RectTransform GetRect() => rect;

        private Cube _dragCube;
        private RectTransform _dragCubeRect;
        private Canvas _canvas;
        private Vector2 _startDragPosition;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startDragPosition = eventData.position;
            _dragCube = _cubesFactory.CreateCube(CubeType, _canvas.transform);
            _dragCubeRect = _dragCube.GetRect();
            _dragCube.EnableRaycasts(false);

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    _canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera,
                    out Vector3 localPoint))
            {
                _dragCubeRect.anchoredPosition = localPoint;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragCubeRect == null)
            {
                return;
            }

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                    eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                _dragCubeRect.anchoredPosition = localPoint;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragCubeRect == null)
                return;

            var screenPoint = eventData.position;
            var cubeInsideRemoveHole = _cubesService.CubeInsideRemoveHole();
            if (cubeInsideRemoveHole)
            {
                _cubesService.RemoveCubeFromTower(this);
                _dragCube.StartFadeAnimation();
                return;
            }

            var placed = false;
            var canPlaceAtTower = _cubesService.CubeCanPlaceAtTower(screenPoint);
            if (canPlaceAtTower)
            {
                _cubesService.TryPlaceAtTowerScreen(CubeType, screenPoint, _startDragPosition);
                placed = true;
            }
            else
            {
                _gameEventsDispatcher.Dispatch(new CubeActionEvent(CubeActionType.CanNotPlace));
            }

            if (!placed)
            {
                _dragCube.StartExplosionTween();
            }
            else
            {
                Destroy(_dragCube.gameObject);
            }

            _dragCube = null;
            _dragCubeRect = null;
        }

        public void StartJumpAnimation(RectTransform targetRect, Vector2 endPosition)
        {
            jumpTween.StartAnimation(targetRect, endPosition);
        }

        public void StartFallAnimation(RectTransform targetRect, float height)
        {
            fallTween.StartAnimation(targetRect, height);
        }

        private void StartExplosionTween()
        {
            explosionTween.StartAnimation(rect);
        }

        private void StartFadeAnimation()
        {
            fadeTween.StartAnimation();
        }

        private void EnableRaycasts(bool enable)
        {
            canvasGroup.blocksRaycasts = enable;
        }
    }
}