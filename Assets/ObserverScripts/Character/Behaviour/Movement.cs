using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ObserverScripts.Character.Behaviour
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        private Coroutine _checkPathStatus;
        public event Action<NavMeshPathStatus> PathStatusUpdateEvent;
        private Vector3 target;
        private bool _pathIsActive;

        public void SetEndPosition(Vector3 target)
        {
            this.target = target;
            agent.SetDestination(target);
            _pathIsActive = true;
            _checkPathStatus = StartCoroutine(CheckPathStatus());
        }

        private void OnDestroy()
        {
            if (_checkPathStatus != null) StopCoroutine(_checkPathStatus);
        }

        private IEnumerator CheckPathStatus()
        {
            while (_pathIsActive)
            {
                yield return new WaitForSeconds(1f);
                if (agent.pathStatus == NavMeshPathStatus.PathPartial) continue;
                var distance = Vector3.Distance(target, transform.position);
                if (distance <= agent.stoppingDistance + 0.5f)
                {
                    PathStatusUpdateEvent?.Invoke(NavMeshPathStatus.PathComplete);
                    _pathIsActive = false;
                }
                else if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    PathStatusUpdateEvent?.Invoke(NavMeshPathStatus.PathInvalid);
                    _pathIsActive = false;
                }
            }
        }
    }
}