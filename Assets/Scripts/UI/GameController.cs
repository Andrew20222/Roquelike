using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private ResourseSlider manaSlider;
        [SerializeField] private ResourseSlider hpSlider;
        [SerializeField] private PlayerContainer playerContainer;
        private void Start()
        {
            playerContainer.ManaHandler.OnUpdateMana += manaSlider.SetValue;
            playerContainer.HealView.OnHealthChangeEvent += hpSlider.SetValue;
        }
    }
}