using System;
using Unit.Player;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerContainer prefab;
        public event Action<PlayerContainer> SpawnPlayerEvent;

        private void Start()
        {
            var instance = Instantiate(prefab);
            SpawnPlayerEvent?.Invoke(instance);
            SpawnPlayerEvent = null;
        }
    }
}