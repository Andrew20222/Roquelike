using System;
using DefaultNamespace;
using Unit.Player;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        public event Action<Container> SpawnPlayerEvent;

        [SerializeField] private Container prefab;
        [SerializeField] private StopController stopController;
        [SerializeField] private Observer dieObserver;
        [SerializeField] private Observer restartObserver;
        private Container _playerInstance;
        private IObserverListenable _restartInteface;

        private void Start()
        {
            _playerInstance = Instantiate(prefab);
            IObserverCallbackable callbackable = dieObserver;
            _restartInteface = restartObserver;
            _playerInstance.DeathBehaviour.DeathEvent += () => callbackable.OnCallback();
            _restartInteface.Subscribe(_playerInstance.Restart);
            _playerInstance.SubscribeStop(stopController.Subscribe);
            stopController.OnStopCallback(false);
            SpawnPlayerEvent?.Invoke(_playerInstance);
            SpawnPlayerEvent = null;
        }

        private void OnDestroy()
        {
            _restartInteface.Unsubscribe(_playerInstance.Restart);
        }
    }
}