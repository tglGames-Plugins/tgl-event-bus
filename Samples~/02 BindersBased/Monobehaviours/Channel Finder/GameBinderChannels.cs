namespace TGL.EventBus.WithBinders.Sample;

public static class GameBinderChannels
{
    public static readonly CombatChannel Combat = new();
    public static readonly UiChannel UI = new();
    // public static readonly AudioChannel Audio = new();
}
