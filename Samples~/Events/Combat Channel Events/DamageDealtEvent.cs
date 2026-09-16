namespace TGL.EventBus.Sample;

/// <summary>
/// Tell Damage is dealt to the player using <see cref="CombatEventChannel"/> event bus
/// </summary>
/// <param name="TargetID">Who felt the damage</param>
/// <param name="Amount">Amount of damage dealt</param>
public readonly record struct DamageDealtEvent(int TargetID, float Amount) : ICombatEventBase, IEvent<CombatEventChannel>;
