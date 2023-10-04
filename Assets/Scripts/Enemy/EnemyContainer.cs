using System;
using Interfaces;
using Unit.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyContainer : MonoBehaviour, IDamageble
    {
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        private IStopObservable _stopObservable;
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        [field:SerializeField] public Transform HeadUp { get; private set; }
        public bool IsEnemy => true;
        public IHealView HealView { get; private set; }

        [SerializeField] private float maxHealthValue;
        
        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }

        public void SetStoppable(IStopObservable stopObservable)
        {
            _stopObservable = stopObservable;
            _stopObservable.Subscribe(enemyMove.UpdateStop);
        }
        public void Prepare(PlayerContainer playerContainer)
        {
            HealView = healthStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealthValue);
            enemyMove.Init(playerContainer.transform);
            enemyAttack.Init(playerContainer.transform, playerContainer);
            HealView.OnDeathEvent += enemyMove.Death;
            HealView.OnDeathEvent += DeathBehaviour.Death;

        }

        private void OnDestroy()
        {
            _stopObservable.Unsubscribe(enemyMove.UpdateStop);
        }
    }
}