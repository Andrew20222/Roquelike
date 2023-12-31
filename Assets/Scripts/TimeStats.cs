﻿using System;
using UnityEngine;

public class TimeStats : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private StopController stopController;
    [SerializeField] private Observer winObserver;
    [SerializeField] private Observer loseObserver;
    [SerializeField] private Observer restartObserver;
    private IObserverCallbackable _winCallbackable;
    private IObserverListenable _loseListenable;
    private IObserverListenable _restartListenable;
    private Timer _timer;
    public event Action TimeIsOverEvent;
    public event Action<float, float> TimeUpdateEvent;

    private void Awake()
    {
        _restartListenable = restartObserver;
        _winCallbackable = winObserver;
        _loseListenable = loseObserver;
        _loseListenable.Subscribe((() => OnStopUpdate(true)));
        _timer = new Timer();
        _timer.TimeIsOver += TimeIsOverEvent;
        _timer.HasBeenUpdated += UpdateTimeCallback;
        _timer.TimeIsOver += _winCallbackable.OnCallback;
        _restartListenable.Subscribe(StartTimer);
        stopController.Subscribe(OnStopUpdate);
    }

    private void UpdateTimeCallback(float current, float max)
    {
        Debug.Log($"Time: {current}");
        TimeUpdateEvent?.Invoke(current, max);
    }

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _timer.StopCountingTime(StopCoroutine);
        _timer.Set(time);
        _timer.StartCountingTime(StartCoroutine);
    }

    private void OnStopUpdate(bool value)
    {
        if (value) _timer.StopCountingTime(StopCoroutine);
        else _timer.StartCountingTime(StartCoroutine);
    }

    private void OnDestroy()
    {
        stopController.Unsubscribe(OnStopUpdate);
        _timer.StopCountingTime(StopCoroutine);
    }
}