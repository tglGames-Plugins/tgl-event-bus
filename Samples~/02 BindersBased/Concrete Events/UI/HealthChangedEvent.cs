namespace TGL.EventBus.WithBinders.Sample;

public readonly record struct HealthChangedEvent(float Normalized) : IHudEventBase;