using Core.Cubes.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Cubes
{
    public class Cube : AbstractCube, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private CanvasGroup canvasGroup;

        [Inject] private readonly CubesFactory _cubesFactory;
        [Inject] private readonly CubesService _cubesService;

        public RectTransform GetRect() => rect;

        private Cube _dragGhost;
        private RectTransform _ghostRect;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragGhost = _cubesFactory.CreateCube(CubeType, _canvas.transform);
            _ghostRect = _dragGhost.GetRect();
            _dragGhost.EnableRaycasts(false);

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    _canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera,
                    out Vector3 localPoint))
            {
                _ghostRect.anchoredPosition = localPoint;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_ghostRect == null)
            {
                return;
            }

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                    eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                _ghostRect.anchoredPosition = localPoint;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_ghostRect == null)
                return;

            var screenPoint = eventData.position;
            var canPlaceAtTower = _cubesService.CubeCanPlaceAtTower(screenPoint);
            if (canPlaceAtTower)
            {
                _cubesService.TryPlaceAtTowerScreen(CubeType, screenPoint);
            }

            Destroy(_ghostRect.gameObject);

            _dragGhost = null;
            _ghostRect = null;
        }

        private void EnableRaycasts(bool enable)
        {
            canvasGroup.blocksRaycasts = enable;
        }
    }
}