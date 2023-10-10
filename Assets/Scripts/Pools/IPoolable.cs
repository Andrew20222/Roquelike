using System;
using UnityEngine;

namespace Pools
{
    public interface IPoolable<T> : ISetStoppable
    {
        public event Action<T> ReturnInPool;
        public void SetPosition(Vector3 position);
    }
}