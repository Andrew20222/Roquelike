using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class StopController : MonoBehaviour, IStopObservable
{
    private List<Action<bool>> _delegates;
    private event Action<bool> StopEvent;

    public void OnStopCallback(bool value) => StopEvent?.Invoke(value);

    public void Subscribe(Action<bool> value)
    {
        _delegates ??= new List<Action<bool>>();
        if (_delegates.Contains(value)) return;
        StopEvent += value.Invoke;
        _delegates.Add(value);
    }

    public void Unsubscribe(Action<bool> value)
    {
        if (_delegates.Contains(value))
        {
            StopEvent = null;
            _delegates.Remove(value);
            foreach (var action in _delegates)
            {
                StopEvent += action;
            }
        }
    }
}