using GameEvents;

namespace UI.Events
{
    public class CubeActionEvent : IGameEvent
    {
        public CubeActionType ActionType { get; set; }

        public CubeActionEvent(CubeActionType actionType)
        {
            ActionType = actionType;
        }
    }
}