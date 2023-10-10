using UnityEngine;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed;
        
        private Transform _player;
        private bool _isDeath;
        private bool _isStop;

        public void Init(Transform player)
        {
            _player = player;
            _isDeath = false;
            _isStop = false;
        }

        public void UpdateStop(bool value) => _isStop = value;
        public void Death() => _isDeath=true;
        private void Update()
        {
            if (_isStop) return;
            if (_isDeath) return;
            Move();   
        }
    
        private void Move()
        {
            var playerPosition = _player.position;
            var enemyPosition = transform.position;
            
            var direction =
                new Vector3((playerPosition - enemyPosition).normalized.x, 0, (playerPosition - enemyPosition).normalized.z);
            var movement = direction * (speed * Time.deltaTime);
            
            characterController.Move(movement);
        }

    }
}