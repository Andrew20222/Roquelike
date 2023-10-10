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
          if(_position != null)  transform.position = _position.position;
        }
    }
}