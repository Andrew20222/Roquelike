using System.Collections;
using Enemy;
using Unit.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner spawner;
        [SerializeField] private DeathBehaviour deathBehaviour;
        [SerializeField] private Pools.Mana manaPool;
        [SerializeField] private Pools.Enemy enemyPool;
        [SerializeField] private Pools.Canvas enemyPoolCanvas;
        [SerializeField] private StopController stopController;
        [SerializeField] private Transform[] spawnsPos;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;
        
        private PlayerContainer _player;
        private Coroutine _spawnCoroutine;
        private bool _isStop;

        private void Awake()
        {
            spawner.SpawnPlayerEvent += player => _player = player;
        }

        private void Start()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
            stopController.StopEvent += UpdateStop;
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
            if(_spawnCoroutine!=null) StopCoroutine(_spawnCoroutine);
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
                
                deathBehaviour.GetManaEvent += manaPool.GetInPool;
                
                var enemy = enemyPool.GetInPool();
                var canvas = enemyPoolCanvas.GetInPool();
                
                enemy.Play(_player);
                enemy.SetStoppable(stopController);
                canvas.Play((EnemyContainer)enemy);
            }
        }
    }
}