using System;

namespace TGL.EventBus.WithBinders
{
    public interface IBinderEventListener<in TSubCategory> where TSubCategory : IBinderBasedEvent
    {
        internal void Seal();
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TSubCategory;
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : struct, TSubCategory;
    }
}