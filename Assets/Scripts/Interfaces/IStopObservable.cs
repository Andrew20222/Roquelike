using System;

namespace Interfaces
{
    public interface IStopObservable
    { 
        void Subscribe(Action<bool> value);
        void Unsubscribe(Action<bool> value);
    }
}