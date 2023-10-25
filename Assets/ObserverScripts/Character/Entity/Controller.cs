using System;
using ObserverScripts.Character.Behaviour;
using ObserverScripts.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace ObserverScripts.Character.Entity
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Movement movement;
        [SerializeField] private Transform home;
        private Transform _currentSubscriber;
        private ActionType _currentActionType;

        private void Awake()
        {
            movement.SetEndPosition(home.position);
        }

        public void SetSubsriberTarget(Transform subscriber)
        {
            movement.SetEndPosition(subscriber.position);
            movement.PathStatusUpdateEvent += OnPathSubscriber;
            _currentSubscriber = subscriber;
        }

        public void SetActionType(ActionType value)
        {
            _currentActionType = value;
        }

        private void OnPathBuild(NavMeshPathStatus value)
        {
            if (value == NavMeshPathStatus.PathComplete) movement.SetEndPosition(home.position);
            movement.PathStatusUpdateEvent -= OnPathBuild;
        }

        private void OnPathSubscriber(NavMeshPathStatus value)
        {
            if (value == NavMeshPathStatus.PathInvalid)
            {
                movement.SetEndPosition(home.position);
            }
            else if (value == NavMeshPathStatus.PathComplete)
            {
                if (_currentSubscriber.TryGetComponent(out IListenable listenable))
                {
                    if (_currentActionType == ActionType.Subscribe)
                    {
                        Debug.Log("Подписал");
                        listenable.Subscribe(GoToBuild);
                    }
                    else
                    {
                        Debug.Log("Отписал");
                        listenable.Unsubscribe(GoToBuild);
                    }

                    movement.SetEndPosition(home.position);
                }
            }

            movement.PathStatusUpdateEvent -= OnPathSubscriber;
        }

        private void GoToBuild(Transform target)
        {
            movement.SetEndPosition(target.position);
            movement.PathStatusUpdateEvent += OnPathBuild;
        }
    }
}