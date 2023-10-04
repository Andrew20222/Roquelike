using System.Collections;
using Enemy;
using Unit.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyContainer prefab;
        [SerializeField] private EnemyCanvas enemyCanvas;
        [SerializeField] private PlayerSpawner spawner;
        [SerializeField] private Pools.Mana manaPool;
        [SerializeField] private Transform[] spawnPos;
        [SerializeField] private StopController stopController;
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
                var currentPoint = Random.Range(0, spawnPos.Length);
                Debug.Log(Vector3.Distance(_player.transform.position, spawnPos[currentPoint].position));
                if (Vector3.Distance(_player.transform.position, spawnPos[currentPoint].position) < minDistanceToSpawn) continue;
                yield return new WaitForSeconds(timeToSpawn);
                if (_isStop) continue;
                var enemy = Instantiate(prefab, spawnPos[currentPoint].position, Quaternion.identity);
                enemy.DeathBehaviour.GetManaEvent += manaPool.GetInPool;
                enemy.SetStoppable(stopController);
                enemy.Prepare(_player);
                enemyCanvas.SpawnSlider(enemy);
            }
        }
    }
}