using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyContainer prefab;
        [SerializeField] private EnemyCanvas enemyCanvas;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private PlayerContainer player;
        [SerializeField] private float enemySpawnCount;
        [SerializeField] private float timeToSpawn = 1f;

        private void Start()
        {
            StartCoroutine(Spawn(enemySpawnCount));
        }

        private IEnumerator Spawn(float spawnCount)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                yield return new WaitForSeconds(timeToSpawn);
                var enemy = Instantiate(prefab,spawnPos.position, Quaternion.identity);  
                enemy.Prepare(player);
                enemyCanvas.SpawnSlider(enemy);
            }
        }
    }
}