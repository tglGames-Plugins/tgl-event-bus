namespace TGL.EventBus.Sample;

/// <summary>
/// Channel for UI related event
/// </summary>
public readonly struct UIModuleEventChannel : IEventChannel<UIModuleEventChannel, IUIEventBase> { }
