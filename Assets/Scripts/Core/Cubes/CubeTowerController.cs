using System.Collections.Generic;
using Core.Cubes.Config;
using Core.Cubes.Services;
using UnityEngine;
using Zenject;

namespace Core.Cubes
{
    public class CubeTowerController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform towerAreaRect;

        [Inject] private readonly CubesService _cubesService;
        [Inject] private readonly CubesFactory _cubesFactory;

        public RectTransform TowerAreaRect => towerAreaRect;
        public Canvas Canvas => canvas;

        private readonly List<RectTransform> _stackedCubes = new();

        private void Awake()
        {
            _cubesService.RegisterCubeTower(this);
        }

        public bool CanPlaceAtTower(Vector2 screenPos)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(TowerAreaRect, screenPos, Canvas.worldCamera))
            {
                return false;
            }

            if (_stackedCubes.Count == 0)
            {
                return true;
            }

            var canPlaceNextCube = CanPlaceNextCube();

            return canPlaceNextCube;
        }

        public void PlaceCube(CubeType cubeType, Vector2 screenPos)
        {
            var cubesCount = _stackedCubes.Count;

            if (cubesCount == 0)
            {
                var newCube = CreateCube(cubeType, transform);
                var newRect = newCube.GetRect();

                newRect.position = screenPos;
            }
            else
            {
                var topRect = _stackedCubes[^1];
                var worldCorners = new Vector3[4];
                topRect.GetWorldCorners(worldCorners);
                var cubeHeight = topRect.rect.height;
                var horizontalOffset = _cubesService.GetCubeSpawnHorizontalOffset();
                var maxOffset = topRect.rect.width * horizontalOffset * 0.5f;
                var randOffset = Random.Range(-maxOffset, maxOffset);
                var topAnch = topRect.anchoredPosition;
                var position = topAnch + new Vector2(randOffset, cubeHeight);
                var newCube = CreateCube(cubeType, transform);
                var newRect = newCube.GetRect();

                newRect.anchoredPosition = position;
            }
        }

        private bool CanPlaceNextCube()
        {
            if (_stackedCubes == null || _stackedCubes.Count == 0)
            {
                return true;
            }

            var topRect = _stackedCubes[^1];
            var cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            var topWorldCorners = new Vector3[4];
            topRect.GetWorldCorners(topWorldCorners);

            var cubeWorldHeight = topRect.rect.height * topRect.lossyScale.y;
            var cubeWorldWidth = topRect.rect.width * topRect.lossyScale.x;
            var topCenterWorld = (topWorldCorners[1] + topWorldCorners[2]) * 0.5f;

            var horizontalOffset = _cubesService.GetCubeSpawnHorizontalOffset();
            var maxOffsetWorld = (cubeWorldWidth * 0.5f) * horizontalOffset;
            var randOffsetWorld = Random.Range(-maxOffsetWorld, maxOffsetWorld);

            var newCubeCenterWorld =
                topCenterWorld + Vector3.up * (cubeWorldHeight) + Vector3.right * randOffsetWorld;

            var halfRight = (Vector3.right * (cubeWorldWidth * 0.5f));
            var halfUp = (Vector3.up * (cubeWorldHeight * 0.5f));
            var cornerTL = newCubeCenterWorld + halfUp - halfRight;
            var cornerTR = newCubeCenterWorld + halfUp + halfRight;
            var cornerBL = newCubeCenterWorld - halfUp - halfRight;
            var cornerBR = newCubeCenterWorld - halfUp + halfRight;

            var screenTL = RectTransformUtility.WorldToScreenPoint(cam, cornerTL);
            var screenTR = RectTransformUtility.WorldToScreenPoint(cam, cornerTR);
            var screenBL = RectTransformUtility.WorldToScreenPoint(cam, cornerBL);
            var screenBR = RectTransformUtility.WorldToScreenPoint(cam, cornerBR);

            Rect allowedScreenRect;
            if (towerAreaRect != null)
            {
                var areaCorners = new Vector3[4];
                towerAreaRect.GetWorldCorners(areaCorners);
                var areaMin = RectTransformUtility.WorldToScreenPoint(cam, areaCorners[0]);
                var areaMax = RectTransformUtility.WorldToScreenPoint(cam, areaCorners[2]);
                allowedScreenRect = new Rect(areaMin.x, areaMin.y, areaMax.x - areaMin.x, areaMax.y - areaMin.y);
            }
            else
            {
                allowedScreenRect = new Rect(0, 0, Screen.width, Screen.height);
            }

            if (!ContainsWithTolerance(allowedScreenRect, screenTL)) return false;
            if (!ContainsWithTolerance(allowedScreenRect, screenTR)) return false;
            if (!ContainsWithTolerance(allowedScreenRect, screenBL)) return false;
            if (!ContainsWithTolerance(allowedScreenRect, screenBR)) return false;

            bool ContainsWithTolerance(Rect rect, Vector2 point, float tolerance = 50f)
            {
                return point.x >= rect.xMin - tolerance &&
                       point.x <= rect.xMax + tolerance &&
                       point.y >= rect.yMin - tolerance &&
                       point.y <= rect.yMax + tolerance;
            }

            return true;
        }

        private Cube CreateCube(CubeType cubeType, Transform root)
        {
            var cube = _cubesFactory.CreateCube(cubeType, root);
            var cubeRect = cube.GetRect();

            _stackedCubes.Add(cubeRect);

            return cube;
        }
    }
}