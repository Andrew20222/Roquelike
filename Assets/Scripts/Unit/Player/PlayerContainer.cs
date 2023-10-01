using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerContainer : MonoBehaviour, IDamageble
    {
        public bool IsEnemy { get; private set; }
        [field:SerializeField] public Magnet Magnet { get; private set; }
        [SerializeField] private float maxMana;
        [SerializeField] private float maxHp;
        [SerializeField] private ManaStats manaStats;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        public IManaHandler ManaHandler { get; private set; }
        public IHealView HealView { get; private set; }


        private void Awake()
        {
            ManaHandler = manaStats;
            HealView = healthStatsBehaviour;
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