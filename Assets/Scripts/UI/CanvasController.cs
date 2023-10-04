using Spawners;
using UnityEngine;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private GameController gameController;
        private void Awake()
        {
            playerSpawner.SpawnPlayerEvent += gameController.SetPlayer;
        }
    }
}
