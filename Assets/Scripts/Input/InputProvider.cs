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
        private PlayerMove _playerMove;

        private void Awake()
        {
            playerSpawner.SpawnPlayerEvent += playerContainer => _playerMove = playerContainer.PlayerMove;
        }

        private void Update()
        {
            if (_playerMove != null)
                _playerMove.Move(new Vector3(
                    UnityEngine.Input.GetAxis(AXIS_HORIZONTAL), 
                    0, 
                    UnityEngine.Input.GetAxis(AXIS_VERTICAL))
                );
        }
    }
}