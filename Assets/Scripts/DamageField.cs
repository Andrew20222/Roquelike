using DefaultNamespace;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(damage);
        }
        
    }
}
