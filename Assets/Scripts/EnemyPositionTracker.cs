using System;
using UnityEngine;

public class EnemyPositionTracker : MonoBehaviour
{
    private Transform _position;

    public void Init(Transform position)
    {
        _position = position;
    }

    public void OnDestroy()
    {
        
    }

    private void Update()
    {
        transform.position = _position.position;
    }
}