using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent characterController;
    private Transform _player;

    public void Init(Transform player)
    {
        _player = player;
    }

    private void Update()
    {
        Move();   
    }
    
    private void Move()
    {
        var position = _player.position;
        var transform1 = transform;
        var position1 = transform1.position;
        Vector3 direction =
            new Vector3((position - position1).normalized.x, 0, (position - position1).normalized.z);
        Vector3 movement = direction * (speed * Time.deltaTime);
        characterController.Move(movement);
    }

}
