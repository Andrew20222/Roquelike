using System;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class RestartController : MonoBehaviour, IRestartObservable
    {
        private event Action<bool> StopEvent;
        
        public void OnRestartCallback(bool value) => StopEvent?.Invoke(value);

        public void Subscribe(Action<bool> value)
        {
            StopEvent += value.Invoke;
        }

        public void Unsubscribe(Action<bool> value)
        {
            StopEvent -= value.Invoke;
        }
    }
}