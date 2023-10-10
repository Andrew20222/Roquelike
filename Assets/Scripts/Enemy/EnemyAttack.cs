using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float distanceToAttack = 1f;
        private Transform _player;
        private IDamageable _damageable;

        public void Init(IDamageable damageable)
        {
            _player = damageable as Transform;
            _damageable = damageable;
        }

        public void AttackPlayer()
        {
            if (_player == null) return;

            var dist = Vector3.Distance(transform.position, _player.position);

            if (!(dist < distanceToAttack)) return;
            if (!_damageable.IsEnemy)
                _damageable.TakeDamage(damage);
        }
    }
}