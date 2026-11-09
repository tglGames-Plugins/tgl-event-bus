namespace TGL.EventBus.WithBinders.Sample;

public readonly record struct PunchEvent(float Force, int TargetID) : IHandToHandCombatEventBase;