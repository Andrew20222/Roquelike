using System;
using Interfaces;
using Pools;
using UnityEngine;

namespace Enemy
{
    public class EnemyCanvas : MonoBehaviour, ICanvasPoolable<EnemyCanvas>
    {
        [SerializeField] private EnemyResourceLinker enemyResourceLinker;
        private IStopObservable _stopObservable;

        public void SpawnSlider(EnemyContainer enemyContainer)
        {

        }

        public event Action<EnemyCanvas> ReturnInPool;
        public void Play()
        {
            
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetCount(int count){ }

        public void SetStoppable(IStopObservable stopObservable)
        {
        }

        public void Play(EnemyContainer enemyContainer)
        {
            var instance = Instantiate(enemyResourceLinker, transform);
            
            instance.EnemyPositionTracker.Init(enemyContainer.HeadUp);
            enemyContainer.HealView.OnHealthChangeEvent += instance.ResourseSlider.SetValue;
            enemyContainer.HealView.OnDeathEvent += () => Destroy(instance.gameObject);
        }
    }
}