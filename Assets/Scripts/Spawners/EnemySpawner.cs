using System;
using System.Collections;
using Enemy;
using Pools;
using Unit.Player;
using UnityEngine;
using Container = Enemy.Container;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner spawner;
        [SerializeField] private Pools.Enemy enemyPool;
        [SerializeField] private EnemyCanvas enemyPoolCanvas;
        [SerializeField] private StopController stopController;
        [SerializeField] private Observer winObserver;
        [SerializeField] private Transform[] spawnsPos;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;
        private IObserverListenable _winObserverListenable;
        private Unit.Player.Container _player;
        private Coroutine _spawnCoroutine;
        private bool _isStop;

        private void Awake()
        {
            spawner.SpawnPlayerEvent += SpawnPlayer;
            _winObserverListenable = winObserver;
            _winObserverListenable.Subscribe(() => UpdateStop(true));
        }

        private void SpawnPlayer(Unit.Player.Container container)
        {
            _player = container;
            stopController.Subscribe(UpdateStop);
            if (_spawnCoroutine == null) _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void UpdateStop(bool value)
        {
            _isStop = value;

            if (_isStop)
            {
                StopCoroutine(_spawnCoroutine);
            }
            else
            {
                _spawnCoroutine = StartCoroutine(Spawn());
            }
        }

        private void OnDestroy()
        {
            spawner.SpawnPlayerEvent -= SpawnPlayer;
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            stopController.Unsubscribe(UpdateStop);
            _winObserverListenable.Unsubscribe(() => UpdateStop(true));
        }

        private IEnumerator Spawn()
        {
            for (;;)
            {
                yield return new WaitForSeconds(timeToSpawn);
                var currentPoint = Random.Range(0, spawnsPos.Length);

                if (_player == null) continue;

                if (Vector3.Distance(_player.transform.position,
                        spawnsPos[currentPoint].position) < minDistanceToSpawn) continue;

                yield return new WaitForSeconds(timeToSpawn);

                if (_isStop) continue;
                var position = spawnsPos[currentPoint].position;
                var enemy = enemyPool.GetInPool();
                var canvas = enemyPoolCanvas.GetInPool();
                var slider = (EnemyResourceLinker)canvas;
                var container = (Container)enemy;
                _winObserverListenable.Subscribe(container.ReturnInPoolCallback);
                container.HealView.OnHealthChangeEvent += slider.ResourseSlider.SetValue;
                slider.EnemyPositionTracker.Init(container.HeadUp);
                container.DeathEvent += slider.Stop;
                enemy.SetPosition(position);
                enemy.Play();
                canvas.Play();
            }
        }
    }
}