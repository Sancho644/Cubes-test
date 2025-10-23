using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class SmartScrollRect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float directionThreshold = 10f;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private CubesScrollPanel cubesScrollPanel;

        private Vector2 _startPos;
        private bool _directionChosen;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = eventData.position;
            _directionChosen = false;
            SetChildrenRaycast(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_directionChosen)
                return;

            var delta = eventData.position - _startPos;

            if (delta.magnitude < directionThreshold)
                return;

            _directionChosen = true;

            var horizontalMove = Mathf.Abs(delta.x) > Mathf.Abs(delta.y);

            if (horizontalMove)
            {
                scrollRect.enabled = true;
                SetChildrenRaycast(false);
            }
            else
            {
                scrollRect.enabled = false;
                SetChildrenRaycast(true);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            scrollRect.enabled = true;
            _directionChosen = false;
        }

        private void SetChildrenRaycast(bool enable)
        {
            cubesScrollPanel.SetCubesRaycastEnabled(enable);
        }
    }
}