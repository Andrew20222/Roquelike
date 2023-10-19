using System;
using Interfaces;
using Pools;
using Unit.Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyContainer : MonoBehaviour, IDamageable, IPoolable<EnemyContainer>
    {
        public event Action<EnemyContainer> ReturnInPool;

        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private float maxHealthValue;
        public event Action DeathEvent;
        public bool isAlive { get; private set; }

        public bool IsEnemy => true;
        public IHealView HealView { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        [field: SerializeField] public Transform HeadUp { get; private set; }
        private IStopObservable _stopObservable;
        private bool _isStop;

        public void Init(Container container)
        {
            HealView = healthStatsBehaviour;
            HealView.OnDeathEvent += HealDeath;
            DeathBehaviour.DeathEvent += Death;
            enemyMove.Init(container.transform);
            enemyAttack.Init(container, container.transform);
            isAlive = false;
        }

        private void Update()
        {
            if (isAlive == false) return;
            if (_isStop) return;
            enemyAttack.AttackPlayer();
            enemyMove.Move();
        }

        private void OnDestroy()
        {
            _stopObservable.Unsubscribe(UpdateStop);
            HealView.OnDeathEvent -= HealDeath;
            DeathBehaviour.DeathEvent -= Death;
        }

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }

        private void UpdateStop(bool value) => _isStop = value;

        private void HealDeath()
        {
            if (isAlive == false) return;
            isAlive = false;
            DeathBehaviour.Death();
            DeathEvent?.Invoke();
            DeathEvent = null;
        }

        private void Death()
        {
            ReturnInPool?.Invoke(this);
        }

        public void SetStoppable(IStopObservable stopObservable)
        {
            _stopObservable = stopObservable;
            _stopObservable.Subscribe(UpdateStop);
        }

        public void Play()
        {
            gameObject.SetActive(true);
            isAlive = true;
            healthStatsBehaviour.SetMaxHealth(maxHealthValue);
        }

        public void Stop()
        {
            gameObject.SetActive(false);
            isAlive = false;
        }


        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}