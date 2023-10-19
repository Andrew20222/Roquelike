using System;
using Interfaces;
using UnityEngine;

public class StopController : MonoBehaviour, IStopObservable
{
    private event Action<bool> StopEvent;
        
    public void OnStopCallback(bool value) => StopEvent?.Invoke(value);

    public void Subscribe(Action<bool> value)
    {
        StopEvent += value.Invoke;
    }

    public void Unsubscribe(Action<bool> value)
    {
        StopEvent -= value.Invoke;
    }
}   