using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        
        private Transform _player;
        private IDamageble _damageble;

        public void Init(Transform player, IDamageble damageble)
        {
            _player = player;
            _damageble = damageble;
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            var dist = Vector3.Distance(transform.position, _player.position);
            if (dist < 1f)
            {
                _damageble.TakeDamage(damage);
            }
        }
    }
}