using UnityEngine;

namespace TGL.EventBus.WithChannel.Sample;

public class TestHandlerWithChannels : MonoBehaviour
{

    private void OnEnable()
    {
        // Subscribe method callbacks to channels
        ChannelFetcher.CombatChannel.Subscribe<DamageDealtEvent>(OnDamageDealt);
        ChannelFetcher.UiChannel.Subscribe<ComboReachedEvent>(OnComboReached);
        ChannelFetcher.AudioChannel.Subscribe<PlayAudioEvent>(OnPlayAudio);
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks or calling dead objects
        ChannelFetcher.CombatChannel.Unsubscribe<DamageDealtEvent>(OnDamageDealt);
        ChannelFetcher.UiChannel.Unsubscribe<ComboReachedEvent>(OnComboReached);
        ChannelFetcher.AudioChannel.Unsubscribe<PlayAudioEvent>(OnPlayAudio);
    }

    // --- Event Handler Methods (Callbacks) ---
    private void OnDamageDealt(DamageDealtEvent eventData)
    {
        Debug.Log($"[Combat] Damage Dealt: {eventData.Amount} to Target ID: {eventData.TargetID}");
    }

    private void OnComboReached(ComboReachedEvent eventData)
    {
        Debug.Log($"[UI] Combo Reached: {eventData.ComboCounter}x with Weapon: {eventData.WeaponType}");
    }

    private void OnPlayAudio(PlayAudioEvent eventData)
    {
        Debug.Log($"[Audio] Playing Audio Clip: {eventData.AudioName}");
    }
}