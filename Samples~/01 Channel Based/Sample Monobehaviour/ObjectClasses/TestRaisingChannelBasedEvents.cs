using UnityEngine;

namespace TGL.EventBus.WithChannel.Sample;

public class TestRaisingChannelBasedEvents : MonoBehaviour
{
    private void Start()
    {
        // Example of raising events on game start
        UserAttacked();
    }

    public void UserAttacked()
    {
        ChannelFetcher.CombatChannel.Publish(new DamageDealtEvent(TargetID: 101, Amount: 45.5f));
        ChannelFetcher.UiChannel.Publish(new ComboReachedEvent(WeaponType: 2, ComboCounter: 5));
        ChannelFetcher.AudioChannel.Publish(new PlayAudioEvent("SFX_SwordHit"));
    }
}