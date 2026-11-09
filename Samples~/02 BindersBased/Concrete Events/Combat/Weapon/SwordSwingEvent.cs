using TGL.EventBus.WithBinders;
namespace TGL.EventBus.WithBinders.Sample;

public readonly record struct SwordSwingEvent(float Damage) : IWeaponCombatEventBase;
