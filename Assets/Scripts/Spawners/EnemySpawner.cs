using System;
using System.Collections;
using Enemy;
using Pools;
using Unit.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner spawner;
        [SerializeField] private Pools.Enemy enemyPool;
        [SerializeField] private EnemyCanvas enemyPoolCanvas;
        [SerializeField] private StopController stopController;
        [SerializeField] private Transform[] spawnsPos;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;

        private Container _player;
        private Coroutine _spawnCoroutine;
        private bool _isStop;

        private void Awake()
        {
            spawner.SpawnPlayerEvent += SpawnPlayer;
        }

        private void SpawnPlayer(Container container)
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
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
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
                var container = (EnemyContainer)enemy;

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