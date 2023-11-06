using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Lose
{
    public class Controller : MonoBehaviour, IHidenable
    {
        public event Action RestartEvent;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(InvokeRestart);
        }

        private void InvokeRestart()
        {
            RestartEvent?.Invoke();
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(InvokeRestart);
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}