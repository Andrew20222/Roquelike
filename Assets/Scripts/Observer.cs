using System;
using UnityEngine;

public class Observer : MonoBehaviour, IObserverCallbackable, IObserverListenable
{
    private event Action Event;

    void IObserverCallbackable.OnCallback() => Event?.Invoke();

    void IObserverListenable.Subscribe(Action value)
    {
        Event += value;
    }

    void IObserverListenable.Unsubscribe(Action value)
    {
        Event -= value;
    }
}

public interface IObserverCallbackable
{
    void OnCallback();
}

public interface IObserverListenable
{
    void Subscribe(Action value);
    void Unsubscribe(Action value);
}