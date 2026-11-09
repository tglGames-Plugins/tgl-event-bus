namespace TGL.EventBus.WithChannel.Sample;

/// <summary>
/// Channel for Combat related event
/// </summary>
public readonly struct CombatEventChannel : IEventChannel<CombatEventChannel, ICombatEventBase> { }
