using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageDelay;
    private List<IDamageble> _actualEnemies;
    private List<IDamageble> _oldEnemies;
    private Coroutine _damageLoop;

    private void Awake()
    {
        _actualEnemies = new List<IDamageble>();
        _damageLoop = StartCoroutine(DamageLoop());
    }

    private void OnDestroy()
    {
        StopCoroutine(_damageLoop);
        _oldEnemies.Clear();
        _actualEnemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
        {
            if (damageble.IsEnemy)
            {
                damageble.TakeDamage(damage);
                _actualEnemies.Add(damageble);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageble damageble))
        {
            if (damageble.IsEnemy)
            {
                if (_actualEnemies.Contains(damageble))
                {
                    _actualEnemies.Remove(damageble);
                }
            }
        }
    }

    private IEnumerator DamageLoop()
    {
        for (;;)
        {
            yield return new WaitForSeconds(damageDelay);
            _oldEnemies = new List<IDamageble>(_actualEnemies);
            foreach (var damageble in _oldEnemies)
            {
                damageble.TakeDamage(damage);
            }
        }
    }
}