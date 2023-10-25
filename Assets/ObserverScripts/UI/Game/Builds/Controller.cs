using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverScripts.UI.Game.Builds
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;
        [SerializeField] private Market market;
        public event Action<Transform> UpdateTargetEvent;

        private void Awake()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var index = i;
                button.onClick.AddListener(() => SendTarget(market.Subscriber));
            }
        }

        private void SendTarget(Transform target)
        {
            UpdateTargetEvent?.Invoke(target);
        }
    }
}