using System;
using DefaultNamespace;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private SphereCollider sphereCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
        {
            if (damageble.IsEnemy)
                damageble.TakeDamage(damage);
        }
    }
}