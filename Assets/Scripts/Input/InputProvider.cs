using UnityEngine;

public class InputProvider : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;

    private void Update()
    {
        playerMove.Move(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")));
    }
}
