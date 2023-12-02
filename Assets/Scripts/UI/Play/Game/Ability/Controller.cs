using System;
using System.Collections.Generic;
using Abilities.Enums;
using Abilities.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Play.Game.Ability
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private List<Abilities.Enums.Ability> missing;
        [SerializeField] private List<BuffInfo> infos;
        [SerializeField] private List<CardInfo> cards;
        [SerializeField] private CanvasGroup canvasGroup;
        private List<Abilities.Enums.Ability> _present;
        private Dictionary<Abilities.Enums.Ability, List<Buff>> _canOffer;
        public event Action<Abilities.Enums.Ability> SendSelectedAbility;
        public event Action<Abilities.Enums.Ability, Buff> SendSelectedBuff;

        private void Awake()
        {
            _present = new List<Abilities.Enums.Ability>();
            _canOffer = new Dictionary<Abilities.Enums.Ability, List<Buff>>();
        }

        public void ProvideChoice()
        {
            foreach (var card in cards)
            {
                var text = "";
                var randomAbility = 0;
                Abilities.Enums.Ability ability;
                Buff buff;
                if (_present.Count == 0)
                {
                    randomAbility = Random.Range(0, missing.Count);
                    ability = missing[randomAbility];
                    text = $"Take {ability.ToString()}";
                    card.SetText(text);
                    card.OnClick += () => SetAbility(missing[randomAbility]);
                }
                else if (_present.Count >= 1)
                {
                    randomAbility = Random.Range(0, _present.Count);
                    ability = _present[randomAbility];
                    var randomBuff = Random.Range(0, _canOffer[ability].Count);
                    buff = _canOffer[ability][randomBuff];
                    text = $"Upgrade {buff.ToString()} in {ability.ToString()}";
                    card.SetText(text);
                    card.OnClick += () => SetAbilityBuf(ability, buff);
                }
            }
        }

        private void SetAbility(Abilities.Enums.Ability value)
        {
            if (missing.Contains(value))
                missing.Remove(value);
            _present.Add(value);
            foreach (var buffInfo in infos)
            {
                if (buffInfo.Ability != value) continue;
                _canOffer.Add(value, buffInfo.CanBuffOffer);
                break;
            }

            SendSelectedAbility?.Invoke(value);
        }

        private void SetAbilityBuf(Abilities.Enums.Ability ability, Buff buff)
        {
            SendSelectedBuff?.Invoke(ability, buff);
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