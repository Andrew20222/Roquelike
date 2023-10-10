using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Pools
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyContainer prefab;
        [SerializeField] private int minCount;
        private Queue<IEnemyPoolable<EnemyContainer>> _instances;

        private void Awake()
        {
            _instances = new Queue<IEnemyPoolable<EnemyContainer>>();
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
        
        private void ReturnInPool(EnemyContainer value)
        {
            value.SetPosition(new Vector3(0,-1,0));
            _instances.Enqueue(value);
        }

        public IEnemyPoolable<EnemyContainer> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }

            return _instances.Dequeue();
        }
    }
}