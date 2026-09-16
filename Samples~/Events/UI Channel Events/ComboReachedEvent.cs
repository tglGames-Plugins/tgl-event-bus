namespace TGL.EventBus.Sample;


/// <summary>
/// Inform UI that we need to update the combo counter using <see cref="UIModuleEventChannel"/> event bus
/// </summary>
/// <param name="WeaponType">The type of weapon dealing the damage</param>
/// <param name="ComboCounter">Combo count for this weapon</param>
public readonly record struct ComboReachedEvent(int WeaponType, int ComboCounter) : IUIEventBase, IEvent<UIModuleEventChannel>;
