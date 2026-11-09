using UnityEngine;

namespace TGL.EventBus.WithBinders.Sample;

public class PunchFXReceiver : MonoBehaviour
{
    // Opt into listen-only access via the narrow interface (cannot Raise from here)
    private IBinderEventListener<IHandToHandCombatEventBase> _handToHandListener => GameBinderChannels.Combat.HandToHand.Listener;

    private void OnEnable()
    {
        _handToHandListener.Subscribe<PunchEvent>(OnPunchExecuted);
    }

    private void OnDisable()
    {
        _handToHandListener.Unsubscribe<PunchEvent>(OnPunchExecuted);
    }

    private void OnPunchExecuted(PunchEvent data)
    {
        Debug.Log($"Playing punch sound for target {data.TargetID} with force {data.Force}");
    }
}