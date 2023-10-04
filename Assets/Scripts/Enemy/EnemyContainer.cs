using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyContainer : MonoBehaviour, IDamageble
    {
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private DeathBehaviour deathBehaviour;
        [field:SerializeField] public Transform HeadUp { get; private set; }
        public bool IsEnemy => true;
        public IHealView HealView { get; private set; }

        [SerializeField] private float maxHealthValue;
        
        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }

        public void Prepare(PlayerContainer playerContainer)
        {
            HealView = healthStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealthValue);
            enemyMove.Init(playerContainer.transform);
            enemyAttack.Init(playerContainer.transform, playerContainer);
            HealView.OnDeathEvent += enemyMove.Death;
            HealView.OnDeathEvent += deathBehaviour.Death;

        }
    }
}