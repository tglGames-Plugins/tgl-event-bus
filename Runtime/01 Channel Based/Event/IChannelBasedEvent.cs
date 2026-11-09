namespace TGL.EventBus.WithChannel
{
    // public interface IEventBase { } // No need of interface for event Base

    /// <summary>
    /// Every event must define which channel it works with
    /// These are the final events that a channel subscribes or unsubscribes to and raises when invoked.
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public interface IChannelBasedEvent<TChannel> { }
}
