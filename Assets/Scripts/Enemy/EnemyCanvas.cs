using System.Collections.Generic;
using Pools;
using UnityEngine;

namespace Enemy
{
    public class EnemyCanvas : MonoBehaviour
    {
        [SerializeField] private EnemyResourceLinker prefab;
        [SerializeField] private int minCount;
        private Queue<IPoolable<EnemyResourceLinker>> _instances;

        private void Awake()
        {
            _instances = new Queue<IPoolable<EnemyResourceLinker>>();
            for (int i = 0; i < minCount; i++)
            {
                AddInstance();
            }
        }

        private void AddInstance()
        {
            var instance = Instantiate(prefab, transform);
            instance.ReturnInPool += ReturnInPool;
            _instances.Enqueue(instance);
        }
        
        private void ReturnInPool(EnemyResourceLinker value)
        {
            _instances.Enqueue(value);
        }

        public IPoolable<EnemyResourceLinker> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }

            return _instances.Dequeue();
        }
    }
}