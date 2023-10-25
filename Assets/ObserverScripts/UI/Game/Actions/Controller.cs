using System;
using System.Collections.Generic;
using ObserverScripts.Character.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverScripts.UI.Game.Actions
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private List<Button> buttons;
        public event Action<ActionType> UpdateActionEvent;

        private void Awake()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var index = i;
                button.onClick.AddListener(() => SendAction(index));
            }
        }

        private void SendAction(int value)
        {
            UpdateActionEvent?.Invoke((ActionType)value);
        }
    }
}