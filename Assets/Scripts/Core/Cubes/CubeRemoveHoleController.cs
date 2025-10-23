using Core.Cubes.Services;
using UnityEngine;
using Zenject;

namespace Core.Cubes
{
    public class CubeRemoveHoleController : MonoBehaviour
    {
        [SerializeField] private RectTransform holeRect;
        [SerializeField] private Canvas canvas;

        [Inject] private readonly CubesService _cubesService;
        
        private void Awake()
        {
            _cubesService.RegisterRemoveHole(this);
        }
        
        public bool IsInsideHole()
        {
            var screenPos = Input.mousePosition;
            var corners = new Vector3[4];
            holeRect.GetWorldCorners(corners);

            var center = (RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[0]) +
                          RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[2])) / 2f;

            var radiusX = Mathf.Abs(corners[2].x - corners[0].x) * 0.5f;
            var radiusY = Mathf.Abs(corners[2].y - corners[0].y) * 0.5f;

            var dx = (screenPos.x - center.x) / radiusX;
            var dy = (screenPos.y - center.y) / radiusY;

            return dx * dx + dy * dy <= 1f;
        }
    }
}