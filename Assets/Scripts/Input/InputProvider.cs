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
        private IObserverListenable _dieListener;
        private PlayerMove _playerMove;
        private bool _isStop;
        private bool _isDie;
        public event Action StopEvent;

        private void Awake()
        {
            _isDie = false;
            _dieListener = dieObserver;
            _dieListener.Subscribe(Die);
            stopController.Subscribe(UpdateStop);
            playerSpawner.SpawnPlayerEvent += playerContainer => _playerMove = playerContainer.PlayerMove;
        }

        private void Die() => _isDie = true;

        private void UpdateStop(bool value)
        {
            _isStop = value;
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
        }
    }
}