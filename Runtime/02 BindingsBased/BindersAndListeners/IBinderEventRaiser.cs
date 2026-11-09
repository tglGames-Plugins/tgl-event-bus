namespace TGL.EventBus.WithBinders
{
    public interface IBinderEventRaiser<in TSubCategory> where TSubCategory : IBinderBasedEvent
    {
        internal void Seal();
        void Raise<TEvent>(TEvent eventData = default) where TEvent : struct, TSubCategory;
    }
}