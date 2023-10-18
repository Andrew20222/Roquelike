using System;
using UnityEngine;

namespace Pools
{
    public interface IPoolable<T>
    {
        public event Action<T> ReturnInPool;
        public void SetPosition(Vector3 position);
        public void Play();
        public void Stop();
    }
}