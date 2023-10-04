using UnityEngine;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private CharacterController characterController;
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
            var position = _player.position;
            var transform1 = transform;
            var position1 = transform1.position;
            Vector3 direction =
                new Vector3((position - position1).normalized.x, 0, (position - position1).normalized.z);
            Vector3 movement = direction * (speed * Time.deltaTime);
            characterController.Move(movement);
        }

    }
}