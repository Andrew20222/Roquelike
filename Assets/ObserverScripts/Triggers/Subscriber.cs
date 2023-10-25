using System;
using UnityEngine;

namespace ObserverScripts.Triggers
{
    public class Subscriber : MonoBehaviour, IListenable
    {
        public event Action<Action<Transform>> SubscribeEvent;
        public event Action<Action<Transform>> UnsubscribeEvent;
        
        void IListenable.Subscribe(Action<Transform> action)
        {
            SubscribeEvent?.Invoke(action);
        }

        void IListenable.Unsubscribe(Action<Transform> action)
        {
            UnsubscribeEvent?.Invoke(action);
        }
    }

    public interface IListenable
    {
        void Subscribe(Action<Transform> action);
        void Unsubscribe(Action<Transform> action);
    }
}