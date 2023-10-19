using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Pause
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button continueButton;

        public event Action ContinueEvent;

        private void Start()
        {
            continueButton.onClick.AddListener(ContinueEvent.Invoke);
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