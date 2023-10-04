using Spawners;
using Unit.Player;
using UnityEngine;

namespace Input
{
    public class InputProvider : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        private PlayerMove _playerMove;

        private void Awake()
        {
            playerSpawner.SpawnPlayerEvent += container => _playerMove = container.PlayerMove;
        }

        private void Update()
        {
            if (_playerMove != null)
                _playerMove.Move(new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical")));
        }
    }
}