using UnityEngine;

namespace TGL.EventBus.Sample;

public class SampleTest2 : MonoBehaviour
{
    private void Start()
    {
        // Example of raising events on game start
        UserAttacked();
    }

    public void UserAttacked()
    {
        ControllerEventBus.CombatChannel.Publish(new DamageDealtEvent(TargetID: 101, Amount: 45.5f));
        ControllerEventBus.UiChannel.Publish(new ComboReachedEvent(WeaponType: 2, ComboCounter: 5));
        ControllerEventBus.AudioChannel.Publish(new PlayAudioEvent("SFX_SwordHit"));
    }
}