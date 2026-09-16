using System;

namespace TGL.EventBus
{
    /// <summary>
    /// Channels define a base event(TBaseEvent) and we only deal with events that are child of TBaseEvent event,
    /// Each Channel will act as a event bus, but uses a centralized event bus in the background.
    /// </summary>
    /// <typeparam name="TSelf">The reference to oneself</typeparam>
    /// <typeparam name="TBaseEvent">Which is the base event which this channel deals with?</typeparam>
    public interface IEventChannel<TSelf, TBaseEvent> where TSelf : struct, IEventChannel<TSelf, TBaseEvent>
    {
        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TBaseEvent, IEvent<TSelf>
        {
            // Will this help? : Action<IEventChannel<TSelf, TBaseEvent>> channelHandler = handler as Action<IEventChannel<TSelf, TBaseEvent>>; 
            EventBus.ChannelSubscribe<TSelf, TEvent>(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TBaseEvent, IEvent<TSelf>
        {
            EventBus.ChannelUnsubscribe<TSelf, TEvent>(handler);
        }

        public void Publish<TEvent>(TEvent eventData = default) where TEvent : struct, TBaseEvent, IEvent<TSelf>
        {
            EventBus.ChannelPublish<TSelf, TEvent>(eventData);
        }
    }
}
