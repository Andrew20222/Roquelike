using Unit.Player;
using UnityEngine;

namespace UI
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ResourseSlider manaSlider;
        [SerializeField] private ResourseSlider hpSlider;
        private PlayerContainer _playerContainer;

        public void SetPlayer(PlayerContainer player)
        {
            _playerContainer = player;
            _playerContainer.ManaHandler.OnUpdateMana += manaSlider.SetValue;
            _playerContainer.HealView.OnHealthChangeEvent += hpSlider.SetValue;
        }
    }
}