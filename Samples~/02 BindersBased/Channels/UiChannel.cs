using TGL.EventBus.WithBinders;
namespace TGL.EventBus.WithBinders.Sample;

public class UiChannel : BinderEventChannel<IUiEventBase>
{
    public RaiserListenerHandler<IHudEventBase> Hud { get; } = CreateHandler<IHudEventBase>();
        
    // COMPILER ERROR: IHandToHandCombatEventBase does not derive from IUiEventBase
    // public RaiserListenerHandler<IHandToHandCombatEventBase> HandToHand { get; } = CreateHandler<IHandToHandCombatEventBase>();
}