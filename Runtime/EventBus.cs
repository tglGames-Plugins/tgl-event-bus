using System;

namespace TGL.EventBus
{
    /// <summary>
    /// The Hidden Event bus that manages all Channels.
    /// The users only talk to the channels and should never interact with this event bus directly.
    /// </summary>
    internal static class EventBus
    {
        private static class EventBindings<TChannel, TEvent> where TEvent : struct, IEvent<TChannel>
        {
            public static Action<TEvent> OnEventRaised;
        }

        internal static void ChannelSubscribe<TChannel, TEvent>(Action<TEvent> handler)
            where TEvent : struct, IEvent<TChannel>
        {
            if (handler == null) return;

            Action<TEvent> current = EventBindings<TChannel, TEvent>.OnEventRaised;

            // Check if handler is already present in the invocation list
            if (current != null && Array.IndexOf(current.GetInvocationList(), handler) >= 0)
            {
                // Debug.LogWarning($"[EventBus] Handler '{handler.Method.Name}' is already subscribed to event '{typeof(TEvent).Name}' on channel '{typeof(TChannel).Name}'. Subscription skipped.");
                return;
            }
            EventBindings<TChannel, TEvent>.OnEventRaised += handler;
        }

        internal static void ChannelUnsubscribe<TChannel, TEvent>(Action<TEvent> handler)
            where TEvent : struct, IEvent<TChannel>
        {
            if (handler == null) return;

            Action<TEvent> current = EventBindings<TChannel, TEvent>.OnEventRaised;

            // Check if handler exists before attempting to remove
            if (current == null || Array.IndexOf(current.GetInvocationList(), handler) < 0)
            {
                // Debug.LogWarning($"[EventBus] Attempted to unsubscribe handler '{handler.Method.Name}' from event '{typeof(TEvent).Name}' on channel '{typeof(TChannel).Name}', but it was not registered.");
                return;
            }
            
            EventBindings<TChannel, TEvent>.OnEventRaised -= handler;
        }

        internal static void ChannelPublish<TChannel, TEvent>(TEvent eventData = default)
            where TEvent : struct, IEvent<TChannel>
        {
            Action<TEvent> current = EventBindings<TChannel, TEvent>.OnEventRaised;

            // Check if handler exists before attempting to remove
            if (current == null || current.GetInvocationList().Length == 0)
            {
                // Debug.LogWarning($"[EventBus] Attempted to unsubscribe handler '{handler.Method.Name}' from event '{typeof(TEvent).Name}' on channel '{typeof(TChannel).Name}', but it was not registered.");
                return;
            }
            EventBindings<TChannel, TEvent>.OnEventRaised?.Invoke(eventData);
        }
    }
}
