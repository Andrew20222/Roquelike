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
        [SerializeField] private Transform[] spawnPos;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;
        private PlayerContainer _player;
        private Coroutine _spawnCoroutine;

        private void Awake()
        {
            spawner.SpawnPlayerEvent += player => _player = player;
        }

        private void Start()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private void OnDestroy()
        {
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator Spawn()
        {
            for (;;)
            {
                var currentPoint = Random.Range(0, spawnPos.Length);
                Debug.Log(Vector3.Distance(_player.transform.position, spawnPos[currentPoint].position));
                if (Vector3.Distance(_player.transform.position, spawnPos[currentPoint].position) < minDistanceToSpawn) continue;
                yield return new WaitForSeconds(timeToSpawn);
                var enemy = Instantiate(prefab, spawnPos[currentPoint].position, Quaternion.identity);
                enemy.Prepare(_player);
                enemyCanvas.SpawnSlider(enemy);
            }
        }
    }
}