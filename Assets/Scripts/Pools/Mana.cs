using System.Collections;
using System.Collections.Generic;
using Mana;
using UnityEngine;

namespace Pools
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] private ManaFiller prefab;
        [SerializeField] private int minCount;
        private Queue<IPoolable<ManaFiller>> _instances;

        private void Awake()
        {
            _instances = new Queue<IPoolable<ManaFiller>>();
            for (int i = 0; i < minCount; i++)
            {
                AddInstance();
            }
        }

        private void AddInstance()
        {
            var instance = Instantiate(prefab);
            instance.ReturnInPool += ReturnInPool;
            _instances.Enqueue(instance);
        }

        private void ReturnInPool(ManaFiller value)
        {
            value.SetPosition(new Vector3(0,-5,0));
            _instances.Enqueue(value);
        }

        public IPoolable<ManaFiller> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }
            return _instances.Dequeue();
        }
    }
}