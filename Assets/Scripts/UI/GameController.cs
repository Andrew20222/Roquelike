using Unit.Player;
using UnityEngine;

namespace UI
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ResourseSlider manaSlider;
        [SerializeField] private ResourseSlider hpSlider;
        private PlayerContainer _playerContainer;

        private void Start()
        {
            _playerContainer.ManaHandler.OnUpdateMana += manaSlider.SetValue;
            _playerContainer.HealView.OnHealthChangeEvent += hpSlider.SetValue;
        }

        public void SetPlayer(PlayerContainer player) =>
            _playerContainer = player;
    }
}