using TGL.EventBus.WithBinders;
namespace TGL.EventBus.WithBinders.Sample;


public class CombatChannel : BinderEventChannel<ICombatEventBase>
{
    public RaiserListenerHandler<IHandToHandCombatEventBase> HandToHand { get; } = CreateHandler<IHandToHandCombatEventBase>();
    public RaiserListenerHandler<IWeaponCombatEventBase> Weapon { get; } = CreateHandler<IWeaponCombatEventBase>();

    // COMPILER ERROR: IHudEventBase does not derive from ICombatEventBase
    // public RaiserListenerHandler<IHudEventBase> Hud { get; } = CreateHandler<IHudEventBase>();
}
