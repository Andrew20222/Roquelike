using System.Collections;
using System.Collections.Generic;
using Abilities;
using Abilities.Enums;
using Abilities.ScriptableObjects;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unit.Behaviors
{
    public class ProtectiveField : AbilityBase<BaseUpgraderInfo>
    {
        [SerializeField] private CircleDrawer view;
        [SerializeField] private SphereCollider sphereCollider;
        private float _damage;
        private float _radius;
        private float _cooldown;
        private List<IDamageable> _actualEnemies;
        private List<IDamageable> _oldEnemies;
        private Coroutine _damageLoop;

        private void Awake()
        {
            _actualEnemies = new List<IDamageable>();
        }

        private void OnDestroy()
        {
            if (_damageLoop != null) StopCoroutine(_damageLoop);
            _oldEnemies?.Clear();
            _actualEnemies?.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.IsEnemy)
                {
                    if (damageable.isAlive)
                    {
                        damageable.TakeDamage(_damage);
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
                yield return new WaitForSeconds(_cooldown);
                if (_isStop) continue;
                _oldEnemies = new List<IDamageable>(_actualEnemies);

                foreach (var damageable in _oldEnemies)
                {
                    damageable.TakeDamage(_damage);
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

        public override void Init()
        {
            _damageLoop = StartCoroutine(DamageLoop());
            view.gameObject.SetActive(true);
        }

        public override void Finish()
        {
            view.gameObject.SetActive(false);
            if (_damageLoop != null) StopCoroutine(_damageLoop);
        }

        protected override void FillLevelsBuff()
        {
            base.FillLevelsBuff();
            foreach (var value in _buffLevelValues.Keys)
            {
                switch (value)
                {
                    case Buff.Cooldown:
                        _cooldown = _buffLevelValues[value][0];
                        break;
                    case Buff.Damage:
                        _damage = _buffLevelValues[value][0];
                        break;
                    case Buff.Radius:
                        _radius = _buffLevelValues[value][0];
                        sphereCollider.radius = _radius;
                        break;
                }
            }
        }

        public override void Upgrade(Buff value)
        {
            var currentLevel = _buffLevelViewers[value];
            currentLevel++;
            if (_buffLevelValues[value].Count <= currentLevel) return;
            switch (value)
            {
                case Buff.Cooldown:
                    _cooldown -= _buffLevelValues[value][currentLevel];
                    break;
                case Buff.Damage:
                    _damage += _buffLevelValues[value][currentLevel];
                    break;
                case Buff.Radius:
                    _radius += _buffLevelValues[value][currentLevel];
                    sphereCollider.radius = _radius;
                    view.DrawCircle();
                    break;
            }

            _buffLevelViewers[value] = currentLevel;
        }
    }
}