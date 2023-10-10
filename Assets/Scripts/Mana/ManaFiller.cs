using System;
using Interfaces;
using Pools;
using UnityEngine;

namespace Mana
{
    public class ManaFiller : MonoBehaviour, IManaPoolable<ManaFiller>
    {
        [field: SerializeField] public int ManaCount { get; private set; }
        public event Action<ManaFiller> ReturnInPool;

        public void Play()
        {
            gameObject.SetActive(true);
        }

        public void Stop()
        {
            ReturnInPool?.Invoke(this);
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetCount(int count)
        {
            ManaCount = count;
        }

        public void SetStoppable(IStopObservable stopObservable)
        {
        }
    }
}