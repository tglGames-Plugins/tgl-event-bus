namespace TGL.EventBus.WithBinders.Sample;

public readonly record struct KickEvent(float Force, bool IsLowKick) : IHandToHandCombatEventBase;