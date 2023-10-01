using DefaultNamespace;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
        {
            if (damageble.IsEnemy)
                damageble.TakeDamage(damage);
        }
    }
}