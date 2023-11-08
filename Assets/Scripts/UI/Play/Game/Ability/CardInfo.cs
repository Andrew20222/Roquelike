using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Play.Game.Ability
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        public event Action OnClick;

        private void Awake()
        {
            button.onClick.AddListener(OnClickCallback);
        }

        public void SetText(string text)
        {
            textMeshPro.SetText(text);
        }

        private void OnClickCallback()
        {
            OnClick?.Invoke();
            OnClick = null;
        }
    }
}