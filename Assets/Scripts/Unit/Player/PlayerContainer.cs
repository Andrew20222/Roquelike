using Interfaces;
using Mana;
using UnityEngine;

namespace Unit.Player
{
    public class PlayerContainer : MonoBehaviour, IDamageble
    {
        [SerializeField] private float maxMana;
        [SerializeField] private float maxHp;
        [SerializeField] private ManaStats manaStats;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [field: SerializeField] public PlayerMove PlayerMove { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        public bool IsEnemy { get; private set; }
        public IManaHandler ManaHandler { get; private set; }
        public IHealView HealView { get; private set; }


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

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }
    }
}