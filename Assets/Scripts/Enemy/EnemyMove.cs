using UnityEngine;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed;

        private Transform _player;

        public void Init(Transform player)
        {
            _player = player;
        }

        public void Move()
        {
            var playerPosition = _player.position;
            var enemyPosition = transform.position;

            var direction =
                new Vector3((playerPosition - enemyPosition).normalized.x, 0,
                    (playerPosition - enemyPosition).normalized.z);
            var movement = direction * (speed * Time.deltaTime);
            characterController.Move(movement);
        }
    }
}