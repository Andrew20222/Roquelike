using UnityEngine;

namespace Unit.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float speed;

        public void Move(Vector3 axis)
        {
            characterController.Move(axis * (speed * Time.deltaTime));
        }
    }
}
