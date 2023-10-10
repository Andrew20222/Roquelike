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
        private bool _isStop;

        public void Init(Transform player, IDamageable damageable)
        {
            _player = player;
            _damageable = damageable;
            _isStop = false;
        }
        
        public void UpdateStop(bool value) => _isStop = value;
        private void Update()
        {
            if(_isStop) return;
            
            AttackPlayer();
        }

        private void AttackPlayer()
        {
            if(_player == null) return;
            
            var dist = Vector3.Distance(transform.position, _player.position);
            
            if (dist < distanceToAttack)
            {
                if(!_damageable.IsEnemy)
                    _damageable.TakeDamage(damage);
            }
        }
    }
}