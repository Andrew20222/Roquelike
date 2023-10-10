using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Pools
{
    public class Canvas : MonoBehaviour
    {
        [SerializeField] private EnemyCanvas prefab;
        [SerializeField] private int minCount;
        private Queue<ICanvasPoolable<EnemyCanvas>> _instances;

        private void Awake()
        {
            _instances = new Queue<ICanvasPoolable<EnemyCanvas>>();
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
        
        private void ReturnInPool(EnemyCanvas value)
        {
            value.SetPosition(new Vector3(0,-1,0));
            _instances.Enqueue(value);
        }

        public ICanvasPoolable<EnemyCanvas> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }

            return _instances.Dequeue();
        }
    }
}