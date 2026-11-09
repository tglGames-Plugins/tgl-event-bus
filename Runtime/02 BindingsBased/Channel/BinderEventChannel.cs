using System;

namespace TGL.EventBus.WithBinders
{
    public abstract class BinderEventChannel<TChannelBase> where TChannelBase : IBinderBasedEvent
    {
        // C# cannot express "derives from X but is not X" as a constraint, so guard it once per
        // closed generic type. Throws on first access, which surfaces immediately in the Editor.
        static BinderEventChannel()
        {
            if (typeof(TChannelBase) == typeof(IBinderBasedEvent))
            {
                throw new InvalidOperationException(
                    $"{nameof(BinderEventChannel<TChannelBase>)} must be closed over a channel-specific interface " +
                    $"derived from {nameof(IBinderBasedEvent)}, not {nameof(IBinderBasedEvent)} itself.");
            }
        }

        // TSubCategory : TChannelBase makes it a compile error to create a handler
        // for any event family that does not belong to this channel
        protected static RaiserListenerHandler<TSubCategory> CreateHandler<TSubCategory>()
            where TSubCategory : TChannelBase
        {
            return new RaiserListenerHandler<TSubCategory>();
        }
    }
}