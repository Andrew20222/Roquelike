using System;
using Abilities.Enums;
using Interfaces;
using Mana;
using Unit.Behaviors;
using Unit.Behaviors.Stats;
using UnityEngine;

namespace Unit.Player
{
    public class Container : MonoBehaviour, IDamageable
    {
        [SerializeField] private ManaStats manaStats;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private Behaviors.Abilities abilities;
        [SerializeField] private float maxMana;
        [SerializeField] private float maxHp;

        public bool isAlive { get; }
        public bool IsEnemy { get; private set; }
        public IManaHandler ManaHandler { get; private set; }
        public IHealView HealView { get; private set; }
        [field: SerializeField] public PlayerMove PlayerMove { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        public Action ResetManaDelegate;
        public Action<Ability> SetAbilityDelegate;
        public Action<Ability, Buff> UpgradeAbilityDelegate;

        private void Awake()
        {
            ManaHandler = manaStats;
            HealView = healthStatsBehaviour;
            HealView.OnDeathEvent += DeathBehaviour.Death;
            IsEnemy = false;
            ResetManaDelegate = ResetMana;
            SetAbilityDelegate = abilities.SetAbility;
            UpgradeAbilityDelegate = abilities.UpgradeAbility;
            ManaHandler.AddMana(maxMana);
        }

        private void Start()
        {
            manaStats.SetMaxValue(maxMana);
            HealView.SetMaxHealth(maxHp);
        }
        
        public void SubscribeStop(Action<Action<bool>> subscribe)
        {
            abilities.SubscribeStop(subscribe);
        }

        public void Restart()
        {
            manaStats.RemoveAllMana();
            HealView.SetMaxHealth(maxHp);
        }

        private void ResetMana()
        {
            manaStats.RemoveAllMana();
        }

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }
    }
}