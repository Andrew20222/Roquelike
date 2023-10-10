using System;
using Unit.Player;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        public event Action<PlayerContainer> SpawnPlayerEvent;
        
        [SerializeField] private PlayerContainer prefab;
        [SerializeField] private StopController stopController;

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