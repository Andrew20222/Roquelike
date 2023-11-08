using System.Collections;
using System.Collections.Generic;
using Better.Attributes.Runtime;
using Enemy;
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
        [SerializeField] private Observer dieObserver;
        [SerializeField] private Observer restartObserver;
        [SerializeField] private Transform[] spawnsPos;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;
        private IObserverListenable _winObserverListenable;
        private IObserverListenable _restartListenable;
        private IObserverListenable _dieListenable;
        private Unit.Player.Container _player;
        private Coroutine _spawnCoroutine;
        private bool _isStop;
        private Dictionary<Container, EnemyResourceLinker> _resourceLinkers;

        private void Awake()
        {
            _resourceLinkers = new Dictionary<Container, EnemyResourceLinker>();
            _dieListenable = dieObserver;
            _restartListenable = restartObserver;
            spawner.SpawnPlayerEvent += SpawnPlayer;
            _winObserverListenable = winObserver;
            _dieListenable.Subscribe(() => UpdateStop(true));
            _restartListenable.Subscribe(() => UpdateStop(false));
            _winObserverListenable.Subscribe(() => UpdateStop(true));
        }

        private bool _isDeactivate;

        [EditorButton]
        public void Deactivate()
        {
            _isDeactivate = true;
        }

        private void SpawnPlayer(Unit.Player.Container container)
        {
            _player = container;
            stopController.Subscribe(UpdateStop);
            if (_spawnCoroutine == null) _spawnCoroutine = StartCoroutine(Spawn());
        }

        [EditorButton]
        public void SpawnEditor()
        {
            var currentPoint = Random.Range(0, spawnsPos.Length);
            var position = spawnsPos[currentPoint].position;
            var enemy = enemyPool.GetInPool();
            var canvas = enemyPoolCanvas.GetInPool();
            var slider = (EnemyResourceLinker)canvas;
            var container = (Container)enemy;
            if (_resourceLinkers.ContainsKey(container))
            {
                SliderUnsubsribe(container);
            }
            _resourceLinkers.Add(container, slider);
            container.HealView.OnHealthChangeEvent += slider.ResourseSlider.SetValue;
            slider.EnemyPositionTracker.Init(container.HeadUp);
            container.ReturnInPool += SliderUnsubsribe;
            enemy.SetPosition(position);
            enemy.Play();
            canvas.Play();
        
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
            _dieListenable.Subscribe(() => UpdateStop(true));
            _restartListenable.Unsubscribe(() => UpdateStop(false));
            _winObserverListenable.Unsubscribe(() => UpdateStop(true));
        }

        private IEnumerator Spawn()
        {
            for (;;)
            {
                if (_isDeactivate) yield break;
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
                if (_resourceLinkers.ContainsKey(container))
                {
                    SliderUnsubsribe(container);
                }
                _resourceLinkers.Add(container, slider);
                container.HealView.OnHealthChangeEvent += slider.ResourseSlider.SetValue;
                slider.EnemyPositionTracker.Init(container.HeadUp);
                container.ReturnInPool += SliderUnsubsribe;
                enemy.SetPosition(position);
                enemy.Play();
                canvas.Play();
            }
        }

        private void SliderUnsubsribe(Container container)
        {
            if (_resourceLinkers.TryGetValue(container, out EnemyResourceLinker value))
            {
                value.EnemyPositionTracker.Init(null);
                container.HealView.OnHealthChangeEvent -= value.ResourseSlider.SetValue;
                value.Stop();
                container.ReturnInPool -= SliderUnsubsribe;
                _resourceLinkers.Remove(container);
            }
        }
    }
}