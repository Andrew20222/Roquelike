using System.Collections.Generic;
using Enemy;
using Spawners;
using Unit.Player;
using UnityEngine;
using Container = Enemy.Container;

namespace Pools
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Container prefab;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private Mana manaPool;
        [SerializeField] private Observer winObserver;
        [SerializeField] private Observer dieObserver;
        [SerializeField] private StopController stopController;
        [SerializeField] private int minCount;
        private Queue<IPoolable<Container>> _instances;
        private Unit.Player.Container container;
        private IObserverListenable _dieListenable;
        private IObserverListenable _winListenable;

        private void Awake()
        {
            _dieListenable = dieObserver;
            _winListenable = winObserver;
            playerSpawner.SpawnPlayerEvent += OnSpawnPlayer;
        }

        private void OnSpawnPlayer(Unit.Player.Container container)
        {
            this.container = container;
            _instances = new Queue<IPoolable<Container>>();
            for (int i = 0; i < minCount; i++)
            {
                AddInstance();
            }
        }

        private void AddInstance()
        {
            var instance = Instantiate(prefab);
            instance.ReturnInPool += ReturnInPool;
            instance.Init(container);
            instance.DeathBehaviour.GetManaEvent += manaPool.GetInPool;
            _winListenable.Subscribe(instance.ReturnInPoolCallback);
            _dieListenable.Subscribe(instance.ReturnInPoolCallback);
            instance.SetStoppable(stopController);
            _instances.Enqueue(instance);
        }

        private void ReturnInPool(Container value)
        {
            value.Stop();
            value.SetPosition(new Vector3(0, -1, 0));
            _instances.Enqueue(value);
        }

        public IPoolable<Container> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }

            return _instances.Dequeue();
        }
    }
}