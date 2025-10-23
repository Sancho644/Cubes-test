using GameEvents;
using UI.Events;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ActionCommentaryPanel : MonoBehaviour
    {
        [SerializeField] private ActionCommentary actionCommentaryPrefab;
        [SerializeField] private RectTransform root;

        [Inject] private readonly IGameEventsDispatcher _gameEventsDispatcher;
        [Inject] private readonly IInstantiator _instantiator;

        private void Awake()
        {
            _gameEventsDispatcher.AddListener<CubeActionEvent>(OnCubeAction);
        }

        private void OnDestroy()
        {
            _gameEventsDispatcher.RemoveListener<CubeActionEvent>(OnCubeAction);
        }

        private void OnCubeAction(CubeActionEvent @event)
        {
            var actionCommentary = _instantiator
                .InstantiatePrefabForComponent<ActionCommentary>(actionCommentaryPrefab, root);
            actionCommentary.Setup(@event.ActionType);
        }
    }
}