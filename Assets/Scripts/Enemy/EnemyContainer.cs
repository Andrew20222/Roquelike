using System;
using Interfaces;
using Pools;
using Unit.Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyContainer : MonoBehaviour, IDamageable, IEnemyPoolable<EnemyContainer>
    {
        public event Action<EnemyContainer> ReturnInPool;
        
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private float maxHealthValue;
        
        public bool IsEnemy => true;
        public IHealView HealView { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        [field:SerializeField] public Transform HeadUp { get; private set; }
        private IStopObservable _stopObservable;

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }

        public void Play(PlayerContainer playerContainer)
        {
            gameObject.SetActive(true);
            
            HealView = healthStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealthValue);
            
            enemyMove.Init(playerContainer.transform);
            enemyAttack.Init(playerContainer.transform, playerContainer);
            
            HealView.OnDeathEvent += enemyMove.Death;
            HealView.OnDeathEvent += DeathBehaviour.Death;
        }

        public void SetStoppable(IStopObservable stopObservable)
        {
            _stopObservable = stopObservable;
            _stopObservable.Subscribe(enemyMove.UpdateStop);
            _stopObservable.Subscribe(enemyAttack.UpdateStop);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        private void OnDestroy()
        {
            _stopObservable.Unsubscribe(enemyMove.UpdateStop);
            _stopObservable.Unsubscribe(enemyAttack.UpdateStop);
        }
    }
}