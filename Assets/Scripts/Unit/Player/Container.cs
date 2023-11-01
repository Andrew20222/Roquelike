using System;
using Interfaces;
using Mana;
using UnityEngine;

namespace Unit.Player
{
    public class Container : MonoBehaviour, IDamageable
    {
        [SerializeField] private ManaStats manaStats;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private DamageField damageField;
        [SerializeField] private float maxMana;
        [SerializeField] private float maxHp;

        public bool isAlive { get; }
        public bool IsEnemy { get; private set; }
        public IManaHandler ManaHandler { get; private set; }
        public IHealView HealView { get; private set; }
        [field: SerializeField] public PlayerMove PlayerMove { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }


        private void Awake()
        {
            ManaHandler = manaStats;
            HealView = healthStatsBehaviour;
            HealView.OnDeathEvent += DeathBehaviour.Death;
            IsEnemy = false;
        }

        private void Start()
        {
            manaStats.SetMaxValue(maxMana);
            HealView.SetMaxHealth(maxHp);
        }
        
        public void SubscribeStop(Action<Action<bool>> subscribe)
        {
            subscribe?.Invoke(damageField.UpdateStop);
        }

        public void Restart()
        {
            manaStats.SetMaxValue(maxMana);
            HealView.SetMaxHealth(maxHp);
        }

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }
    }
}