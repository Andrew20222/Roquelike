using System;
using Abilities.Enums;
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
        [SerializeField] private Ability.Controller abilityChanger;
        private Container container;
        private RechangePanel _rechangePanel;
        public Action<float, float> OnTimeChanged => timeSlider.SetValue;
        public event Action<bool> ShowAbilityEvent;
        public event Action<Abilities.Enums.Ability> SendAbilitySelected;
        public event Action<Abilities.Enums.Ability, Buff> SendSelectedBuff;

        private void Awake()
        {
            _rechangePanel = new RechangePanel();
            abilityChanger.SendSelectedAbility += (ability =>
            {
                Debug.Log(ability.ToString());
                _rechangePanel.SetPanel(null);
                ShowAbilityEvent?.Invoke(false);
                SendAbilitySelected?.Invoke(ability);
            });
            abilityChanger.SendSelectedBuff += (ability, buff) =>
            {
                _rechangePanel.SetPanel(null);
                ShowAbilityEvent?.Invoke(false);
                SendSelectedBuff?.Invoke(ability, buff);
            };
        }

        public void SetPlayer(Container player)
        {
            container = player;
            container.ManaHandler.OnUpdateMana += manaSlider.SetValue;
            container.ManaHandler.OnUpdateMana += ShowAbilityChanger;
            container.HealView.OnHealthChangeEvent += hpSlider.SetValue;
        }

        private void ShowAbilityChanger(float current, float max)
        {
            if (current < max) return;
            _rechangePanel.SetPanel(abilityChanger);
            abilityChanger.ProvideChoice();
            ShowAbilityEvent?.Invoke(true);
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

        private void OnDestroy()
        {
            container.ManaHandler.OnUpdateMana -= manaSlider.SetValue;
            container.ManaHandler.OnUpdateMana -= ShowAbilityChanger;
            container.HealView.OnHealthChangeEvent -= hpSlider.SetValue;
        }
    }
}