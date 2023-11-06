using System;
using System.Collections;
using Interfaces;
using Pools;
using Unit.Behaviors.Stats;
using Unit.Player;
using UnityEngine;

namespace Enemy
{
    public class Container : MonoBehaviour, IDamageable, IPoolable<Container>
    {
        public event Action<Container> ReturnInPool;

        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private GameObject view;
        [SerializeField] private CapsuleCollider collider;
        [SerializeField] private CharacterController controller;
        [SerializeField] private float maxHealthValue;
        public event Action DeathEvent;
        public bool isAlive { get; private set; }

        public bool IsEnemy => true;
        public IHealView HealView { get; private set; }
        [field: SerializeField] public DeathBehaviour DeathBehaviour { get; private set; }
        [field: SerializeField] public Transform HeadUp { get; private set; }
        private IStopObservable _stopObservable;
        private Coroutine _attackCoroutine;
        private bool _isStop;
        private bool _isReturned;

        public void Init(Unit.Player.Container container)
        {
            HealView = healthStatsBehaviour;
            HealView.OnDeathEvent += HealDeath;
            DeathBehaviour.DeathEvent += Death;
            enemyMove.Init(container.transform);
            enemyAttack.Init(container, container.transform);
            _attackCoroutine = StartCoroutine(Attack());
            isAlive = false;
        }

        private void Update()
        {
            if (isAlive == false) return;
            if (_isStop) return;
            if (_isReturned) return;
            enemyMove.Move();
        }

        private void OnDestroy()
        {
            _stopObservable.Unsubscribe(UpdateStop);
            HealView.OnDeathEvent -= HealDeath;
            DeathBehaviour.DeathEvent -= Death;
            if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
        }

        public void TakeDamage(float damage)
        {
            HealView.SpendHealth(damage);
        }

        private void UpdateStop(bool value) => _isStop = value;

        private void HealDeath()
        {
            if (isAlive == false) return;
            isAlive = false;
            DeathBehaviour.Death();
            DeathEvent?.Invoke();
            DeathEvent = null;
        }

        private void Death()
        {
            ReturnInPoolCallback();
        }

        public void ReturnInPoolCallback()
        {
            if (_isReturned) return;
            ReturnInPool?.Invoke(this);
            _isReturned = true;
        }

        public void SetStoppable(IStopObservable stopObservable)
        {
            _stopObservable = stopObservable;
            _stopObservable.Subscribe(UpdateStop);
        }

        public void Play()
        {
            view.SetActive(true);
            healthStatsBehaviour.SetMaxHealth(maxHealthValue);
            isAlive = true;
            _isReturned = false;
            collider.enabled = true;
            controller.enabled = true;
            Debug.Log($"Enemy Play in Pos {transform.position}");
        }

        public void Stop()
        {
            view.SetActive(false);
            isAlive = false;
            _isReturned = true;
            collider.enabled = false;
            controller.enabled = false;
            Debug.Log($"Enemy Stop");
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (isAlive == false) continue;
                if (_isStop) continue;
                if (_isReturned) continue;
                enemyAttack.AttackPlayer();
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}