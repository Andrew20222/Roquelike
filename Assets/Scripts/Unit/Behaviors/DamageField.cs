using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageDelay;
    private List<IDamageable> _actualEnemies;
    private List<IDamageable> _oldEnemies;
    private Coroutine _damageLoop;
    private bool _isStop;

    private void Awake()
    {
        _actualEnemies = new List<IDamageable>();
        _damageLoop = StartCoroutine(DamageLoop());
    }

    public void UpdateStop(bool value) => _isStop = value;
    private void OnDestroy()
    {
        StopCoroutine(_damageLoop);
        _oldEnemies.Clear();
        _actualEnemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.IsEnemy)
            {
                if (damageable.isAlive)
                {
                    damageable.TakeDamage(damage);
                    _actualEnemies.Add(damageable);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageble))
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
            if(_isStop) continue;
            _oldEnemies = new List<IDamageable>(_actualEnemies);

            foreach (var damageable in _oldEnemies)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    private void Update()
    {
        var removeEnemies = new List<IDamageable>();
        foreach (var enemy in _actualEnemies)
        {
            if (enemy.isAlive == false) removeEnemies.Add(enemy);
        }

        foreach (var enemy in removeEnemies)
        {
            if (_actualEnemies.Contains(enemy)) _actualEnemies.Remove(enemy);
        }
    }
}