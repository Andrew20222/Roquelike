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
            restartButton.onClick.AddListener(RestartEvent.Invoke);
        }

        private void OnDestroy()
        {
            if(RestartEvent != null)
                restartButton.onClick.RemoveListener(RestartEvent.Invoke);
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