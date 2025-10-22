using System;
using JetBrains.Annotations;

namespace GameEvents
{
    public delegate void GameEventHandler<in TEvent>([NotNull] TEvent @event)
        where TEvent : IGameEvent;

    public interface IGameEventsDispatcher : IDisposable
    {
        public void AddListener<TEvent>([NotNull] GameEventHandler<TEvent> handler)
            where TEvent : IGameEvent;
        public void RemoveListener<TEvent>([NotNull] GameEventHandler<TEvent> handler)
            where TEvent : IGameEvent;
        public void Dispatch<TEvent>([NotNull] TEvent @event) where TEvent : IGameEvent;
        public void DispatchOnUpdate<TEvent>([NotNull] TEvent @event) where TEvent : IGameEvent;
        public void InvokeEventInQueue();
        public bool HasEventInQueue();
    }
}