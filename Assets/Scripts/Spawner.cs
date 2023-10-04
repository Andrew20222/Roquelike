using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyContainer prefab;
        [SerializeField] private EnemyCanvas enemyCanvas;
        [SerializeField] private Transform[] spawnPos;
        [SerializeField] private PlayerContainer player;
        [SerializeField] private float timeToSpawn = 1f;
        [SerializeField] private float minDistanceToSpawn = 5f;
        
        private Coroutine _spawnCoroutine;

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
            for (int i = 0;;)
            {
                Debug.Log(Vector3.Distance(player.transform.position, spawnPos[i].position));
                if (Vector3.Distance(player.transform.position, spawnPos[i].position) < minDistanceToSpawn) yield return null;
                yield return new WaitForSeconds(timeToSpawn);
                var currentPoint = Random.Range(0, spawnPos.Length);
                var enemy = Instantiate(prefab, spawnPos[currentPoint].position, Quaternion.identity);
                enemy.Prepare(player);
                enemyCanvas.SpawnSlider(enemy);
            }
        }
    }
}