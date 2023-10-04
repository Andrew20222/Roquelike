using System;
using Unit.Player;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerContainer prefab;
        [SerializeField] private StopController stopController;
        public event Action<PlayerContainer> SpawnPlayerEvent;

        private void Start()
        {
            var instance = Instantiate(prefab);
            instance.DeathBehaviour.DeathEvent += () => stopController.OnStopCallback(true);
            stopController.OnStopCallback(false);
            SpawnPlayerEvent?.Invoke(instance);
            SpawnPlayerEvent = null;
        }
    }
}