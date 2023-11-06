using System;
using Spawners;
using Unit.Player;
using UnityEngine;

namespace Input
{
    public class InputProvider : MonoBehaviour
    {
        private const string AXIS_HORIZONTAL = "Horizontal";
        private const string AXIS_VERTICAL = "Vertical";

        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private StopController stopController;
        [SerializeField] private Observer dieObserver;
        [SerializeField] private Observer winObserver;
        [SerializeField] private Observer restartObserver;
        private IObserverListenable _dieListener;
        private IObserverListenable _winListener;
        private IObserverListenable _restartListenable;
        private PlayerMove _playerMove;
        private bool _isStop;
        private bool _isDie;

        private void Awake()
        {
            _isDie = false;
            _restartListenable = restartObserver;
            _dieListener = dieObserver;
            _winListener = winObserver;
            _winListener.Subscribe(() => UpdateStop(true));
            _restartListenable.Subscribe(Restart);
            _dieListener.Subscribe(Die);
            stopController.Subscribe(UpdateStop);
            playerSpawner.SpawnPlayerEvent += playerContainer => _playerMove = playerContainer.PlayerMove;
        }

        private void Die() => _isDie = true;

        private void UpdateStop(bool value)
        {
            _isStop = value;
        }

        private void Restart()
        {
            _isStop = false;
            _isDie = false;
        }

        private void Update()
        {
            if (_isDie) return;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                stopController.OnStopCallback(!_isStop);
            }

            if (_isStop) return;
            if (_playerMove != null)
                _playerMove.Move(new Vector3(
                    UnityEngine.Input.GetAxis(AXIS_HORIZONTAL),
                    0,
                    UnityEngine.Input.GetAxis(AXIS_VERTICAL))
                );
        }

        private void OnDestroy()
        {
            _dieListener.Unsubscribe(Die);
            stopController.Unsubscribe(UpdateStop);
            _winListener.Unsubscribe(() => UpdateStop(true));
            _restartListenable.Unsubscribe(Restart);
        }
    }
}