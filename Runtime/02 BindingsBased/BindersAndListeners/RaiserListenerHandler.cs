using System;

namespace TGL.EventBus.WithBinders
{
    public sealed class RaiserListenerHandler<TSubCategory> : IBinderEventRaiser<TSubCategory>, IBinderEventListener<TSubCategory>
        where TSubCategory : IBinderBasedEvent
    {
        // Only an EventChannel may create handlers, guaranteeing TSubCategory belongs to that channel
        internal RaiserListenerHandler() { }

        // Restricted views for consumers that should only produce or only consume
        public IBinderEventRaiser<TSubCategory> Raiser => this;
        public IBinderEventListener<TSubCategory> Listener => this;

        void IBinderEventRaiser<TSubCategory>.Seal() { }
        void IBinderEventListener<TSubCategory>.Seal() { }

        public void Raise<TEvent>(TEvent eventData = default) where TEvent : struct, TSubCategory
        {
            BinderBasedEventBus.Publish(eventData);
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TSubCategory
        {
            BinderBasedEventBus.Subscribe(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TSubCategory
        {
            BinderBasedEventBus.Unsubscribe(handler);
        }
    }
}