namespace TGL.EventBus.WithChannel.Sample;

/// <summary>
/// for playing a specific Audio in app using <see cref="AudioModuleEventChannel"/> event bus
/// </summary>
/// <param name="AudioName">Name of the Audio to play</param>
public readonly record struct PlayAudioEvent(string AudioName) : IAudioEventBase, IChannelBasedEvent<AudioModuleEventChannel>;
