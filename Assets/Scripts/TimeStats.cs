using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimeStats : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private StopController stopController;
        [SerializeField] private Observer winObserver;
        [SerializeField] private Observer loseObserver;
        private IObserverCallbackable _observerCallbackable;
        private IObserverListenable _loseListenable;
        private Timer _timer;
        public event Action TimeIsOverEvent;
        public event Action<float, float> TimeUpdateEvent;

        private void Awake()
        {
            _observerCallbackable = winObserver;
            _loseListenable = loseObserver;
            _loseListenable.Subscribe((() => OnStopUpdate(true)));
            _timer = new Timer();
            _timer.TimeIsOver += TimeIsOverEvent;
            _timer.HasBeenUpdated += TimeUpdateEvent;
            _timer.TimeIsOver += _observerCallbackable.OnCallback;
        }

        private void Start()
        {
            stopController.Subscribe(OnStopUpdate);
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
}