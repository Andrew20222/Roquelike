using System;

namespace Interfaces
{
    public interface IRestartObservable
    {
        void Subscribe(Action<bool> value);
        void Unsubscribe(Action<bool> value);
    }
}