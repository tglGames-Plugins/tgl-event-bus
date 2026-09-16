namespace TGL.EventBus
{
    /// <summary>
    /// Every event must define which channel it works with
    /// These are the final events that a channel subscribes or unsubscribes to and raises when invoked.
    /// </summary>
    /// <typeparam name="TChannel"></typeparam>
    public interface IEvent<TChannel> { }
}
