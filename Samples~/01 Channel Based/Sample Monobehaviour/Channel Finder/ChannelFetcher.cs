namespace TGL.EventBus.WithChannel.Sample;

/// <summary>
/// Defines all Channels we will use in the project
/// </summary>
public static class ChannelFetcher
{
    // Declare as the interface type instead of the concrete struct
    public static readonly IEventChannel<CombatEventChannel, ICombatEventBase> CombatChannel = new CombatEventChannel();
    public static readonly IEventChannel<UIModuleEventChannel, IUIEventBase> UiChannel = new UIModuleEventChannel();
    public static readonly IEventChannel<AudioModuleEventChannel, IAudioEventBase> AudioChannel = new AudioModuleEventChannel();
}
