using UnityEngine;

namespace TGL.EventBus.Sample;

public class SampleTest : MonoBehaviour
{

    private void OnEnable()
    {
        // Subscribe method callbacks to channels
        ControllerEventBus.CombatChannel.Subscribe<DamageDealtEvent>(OnDamageDealt);
        ControllerEventBus.UiChannel.Subscribe<ComboReachedEvent>(OnComboReached);
        ControllerEventBus.AudioChannel.Subscribe<PlayAudioEvent>(OnPlayAudio);
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks or calling dead objects
        ControllerEventBus.CombatChannel.Unsubscribe<DamageDealtEvent>(OnDamageDealt);
        ControllerEventBus.UiChannel.Unsubscribe<ComboReachedEvent>(OnComboReached);
        ControllerEventBus.AudioChannel.Unsubscribe<PlayAudioEvent>(OnPlayAudio);
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