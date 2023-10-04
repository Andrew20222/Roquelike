using System;
using Pools;
using UnityEngine;

namespace Mana
{
    public class ManaFiller : MonoBehaviour, IPoolable<ManaFiller>
    {
        [field: SerializeField] public int ManaCount { get; private set; }
        public event Action<ManaFiller> ReturnInPool;
        public void Play()
        {
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void PlayDestroyAnimation()
        {
            ReturnInPool?.Invoke(this);
            gameObject.SetActive(false);
        }

    }
}