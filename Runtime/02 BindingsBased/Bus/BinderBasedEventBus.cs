using System;

namespace TGL.EventBus.WithBinders
{
    internal static class BinderBasedEventBus
    {
        private static class EventBindings<TEvent> where TEvent : struct, IBinderBasedEvent
        {
            public static Action<TEvent> PublishableEvent;
        }

        public static void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IBinderBasedEvent
        {
            if (handler == null) return;

            Action<TEvent> currPubEvent = EventBindings<TEvent>.PublishableEvent;

            // Check if handler is already present in the invocation list
            if (currPubEvent != null && Array.IndexOf(currPubEvent.GetInvocationList(), handler) >= 0)
            {
                // Debug.LogWarning($"[BinderBasedEventBus] Handler '{handler.Method.Name}' is already subscribed to event '{typeof(TEvent).Name}'. Subscription skipped.");
                return;
            }
            EventBindings<TEvent>.PublishableEvent += handler;   
        }

        public static void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, IBinderBasedEvent
        {
            if (handler == null) return;

            Action<TEvent> currPubEvent = EventBindings<TEvent>.PublishableEvent;

            // Check if handler exists before attempting to remove
            if (currPubEvent == null || Array.IndexOf(currPubEvent.GetInvocationList(), handler) < 0)
            {
                // Debug.LogWarning($"[BinderBasedEventBus] Attempted to unsubscribe handler '{handler.Method.Name}' from event '{typeof(TEvent).Name}', but it was not registered.");
                return;
            }
            
            EventBindings<TEvent>.PublishableEvent -= handler;
        }

        public static void Publish<TEvent>(TEvent eventData) where TEvent : struct, IBinderBasedEvent
        {
            Action<TEvent> currPubEvent = EventBindings<TEvent>.PublishableEvent;

            // Check if handler exists before attempting to remove
            if (currPubEvent == null || currPubEvent.GetInvocationList().Length == 0)
            {
                // Debug.LogWarning($"[BinderBasedEventBus] Attempted to publish data '{eventData.GetType().Name}' for event '{typeof(TEvent).Name}', but it had no handlers.");
                return;
            }
            EventBindings<TEvent>.PublishableEvent?.Invoke(eventData);
        }
    }
}