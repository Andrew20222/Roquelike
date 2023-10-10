using UnityEngine;

namespace Enemy
{
    public class EnemyPositionTracker : MonoBehaviour
    {
        private Transform _position;

        public void Init(Transform position)
        {
            _position = position;
        }

        private void Update()
        {
            transform.position = _position.position;
        }
    }
}