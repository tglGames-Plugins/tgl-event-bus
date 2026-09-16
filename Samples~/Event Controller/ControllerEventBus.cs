namespace TGL.EventBus.Sample;

/// <summary>
/// Defines all Channels we will use in the project
/// </summary>
public static class ControllerEventBus
{
    // Declare as the interface type instead of the concrete struct
    public static readonly IEventChannel<CombatEventChannel, ICombatEventBase> CombatChannel = new CombatEventChannel();
    public static readonly IEventChannel<UIModuleEventChannel, IUIEventBase> UiChannel = new UIModuleEventChannel();
    public static readonly IEventChannel<AudioModuleEventChannel, IAudioEventBase> AudioChannel = new AudioModuleEventChannel();
}
