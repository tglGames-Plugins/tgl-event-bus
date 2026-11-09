using UnityEngine;

namespace TGL.EventBus.WithBinders.Sample;

public class PlayerMelee : MonoBehaviour
{
    // Single handler exposes both raise and listen rights
    private IBinderEventRaiser<IHandToHandCombatEventBase> _handToHand => GameBinderChannels.Combat.HandToHand.Raiser;

    public void PerformPunch()
    {
        // Compile-time safe: Can ONLY raise events inheriting from IHandToHandCombatEventBase
        _handToHand.Raise(new PunchEvent(Force: 15.0f, TargetID: 42));
        
        // COMPILER ERROR: SwordSwingEvent does not inherit from IHandToHandCombatEventBase!
        // _handToHand.Raise(new SwordSwingEvent(50f)); 
    }
}