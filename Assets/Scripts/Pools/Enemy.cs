using System.Collections.Generic;
using Enemy;
using Spawners;
using Unit.Player;
using UnityEngine;

namespace Pools
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyContainer prefab;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private Mana manaPool;
        [SerializeField] private StopController stopController;
        [SerializeField] private int minCount;
        private Queue<IPoolable<EnemyContainer>> _instances;
        private PlayerContainer _playerContainer;

        private void Awake()
        {
            playerSpawner.SpawnPlayerEvent += OnSpawnPlayer;
        }

        private void OnSpawnPlayer(PlayerContainer container)
        {
            _playerContainer = container;
            _instances = new Queue<IPoolable<EnemyContainer>>();
            for (int i = 0; i < minCount; i++)
            {
                AddInstance();
            }
        }
        private void AddInstance()
        {
            var instance = Instantiate(prefab);
            instance.ReturnInPool += ReturnInPool;
            instance.Init(_playerContainer);
            instance.DeathBehaviour.GetManaEvent += manaPool.GetInPool;
            instance.SetStoppable(stopController);
            _instances.Enqueue(instance);
        }
        
        private void ReturnInPool(EnemyContainer value)
        {
            value.Stop();
            value.SetPosition(new Vector3(0,-1,0));
            _instances.Enqueue(value);
        }

        public IPoolable<EnemyContainer> GetInPool()
        {
            if (_instances.Count == 0)
            {
                AddInstance();
            }

            return _instances.Dequeue();
        }
    }
}