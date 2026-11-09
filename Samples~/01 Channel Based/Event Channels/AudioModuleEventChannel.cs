namespace TGL.EventBus.WithChannel.Sample;

/// <summary>
/// Channel for Audio play and stop
/// </summary>
public readonly struct AudioModuleEventChannel : IEventChannel<AudioModuleEventChannel, IAudioEventBase> { }
