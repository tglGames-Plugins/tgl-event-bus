namespace TGL.EventBus.WithChannel.Sample;

/// <summary>
/// Channel for UI related event
/// </summary>
public readonly struct UIModuleEventChannel : IEventChannel<UIModuleEventChannel, IUIEventBase> { }
