using System;
using Unit.Player;
using UnityEngine;

namespace UI.Play.Game
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private ResourseSlider manaSlider;
        [SerializeField] private ResourseSlider hpSlider;
        [SerializeField] private ResourseSlider timeSlider;
        [SerializeField] private CanvasGroup canvasGroup;
        private Container container;
        public Action<float, float> OnTimeChanged => timeSlider.SetValue;

        public void SetPlayer(Container player)
        {
            container = player;
            container.ManaHandler.OnUpdateMana += manaSlider.SetValue;
            container.HealView.OnHealthChangeEvent += hpSlider.SetValue;
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