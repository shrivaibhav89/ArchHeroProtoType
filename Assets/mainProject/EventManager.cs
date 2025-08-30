using System;
using System.Collections.Generic;

public interface IGameEventType { }

public struct GameEvent : IGameEventType
{
    public static readonly GameEvent OnEnemyHit = new GameEvent();
    public static readonly GameEvent OnEnemyDeath = new GameEvent();
    public static readonly GameEvent OnWeaponFire = new GameEvent();
    public static readonly GameEvent OnWeaponReload = new GameEvent();
    // Add more events here as needed
}
public struct UIEvent : IGameEventType
{
    public static readonly UIEvent OnHealthChanged = new UIEvent();
    public static readonly UIEvent OnAmmoChanged = new UIEvent();
    // Add more UI events here as needed
}

public struct PlayerEvent : IGameEventType
{
    public static readonly PlayerEvent OnPlayerHit = new PlayerEvent();
    public static readonly PlayerEvent OnPlayerDeath = new PlayerEvent();
    public static readonly PlayerEvent OnPlayerRespawn = new PlayerEvent();
}

public static class EventManager
{
    private static Dictionary<IGameEventType, Action<object>> eventDictionary = new Dictionary<IGameEventType, Action<object>>();

    public static void StartListening<T>(T eventType, Action<object> listener) where T : struct, IGameEventType
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = delegate { };
        }
        eventDictionary[eventType] += listener;
    }

    public static void StopListening<T>(T eventType, Action<object> listener) where T : struct, IGameEventType
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
        }
    }

    public static void TriggerEvent<T>(T eventType, object parameter = null) where T : struct, IGameEventType
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType].Invoke(parameter);
        }
    }
}
