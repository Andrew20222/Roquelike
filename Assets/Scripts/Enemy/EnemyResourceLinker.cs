using System;
using Pools;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyResourceLinker : MonoBehaviour, IPoolable<EnemyResourceLinker>
    {
        [field:SerializeField] public ResourseSlider ResourseSlider { get; private set; }
        [field:SerializeField] public EnemyPositionTracker EnemyPositionTracker { get; private set; }
        public event Action<EnemyResourceLinker> ReturnInPool;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Play()
        {
            gameObject.SetActive(true);
        }

        public void Stop()
        {
            gameObject.SetActive(false);
            ReturnInPool?.Invoke(this);
        }
    }
}