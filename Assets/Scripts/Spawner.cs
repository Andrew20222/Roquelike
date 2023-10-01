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
                yield return new WaitForSeconds(timeToSpawn);
                var currentPoint = Random.Range(0, spawnPos.Length);
                var enemy = Instantiate(prefab, spawnPos[currentPoint].position, Quaternion.identity);
                enemy.Prepare(player);
                enemyCanvas.SpawnSlider(enemy);
            }
        }
    }
}