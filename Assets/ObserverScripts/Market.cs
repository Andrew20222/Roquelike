using System;
using ObserverScripts.Triggers;
using UnityEngine;

namespace ObserverScripts
{
    public class Market : MonoBehaviour
    {
        [SerializeField] private Subscriber subscriber;
        private event Action<Transform> OnUpdate;
        public Transform Subscriber => subscriber.transform;

        private void Awake()
        {
            subscriber.SubscribeEvent += Subscribe;
            subscriber.UnsubscribeEvent += Unsubscribe;
        }

        public void SendUpdateSubscribers()
        {
            OnUpdate?.Invoke(transform);
        }

        private void Subscribe(Action<Transform> action)
        {
            OnUpdate += action.Invoke;
        }

        private void Unsubscribe(Action<Transform> action)
        {
            OnUpdate = null;
        }
    }
}