using Cinemachine;
using Spawners;
using UnityEngine;

namespace Unit.Player
{
   public class PlayerCameraListener : MonoBehaviour
   {
      [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
      [SerializeField] private PlayerSpawner playerSpawner;

      private void Awake()
      {
         playerSpawner.SpawnPlayerEvent += FillFields;
      }

      private void FillFields(Container player)
      {
         var transformPlayer = player.transform;
         cinemachineVirtualCamera.Follow = transformPlayer;
         cinemachineVirtualCamera.LookAt = transformPlayer;
      }
   }
}
