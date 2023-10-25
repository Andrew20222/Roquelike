using UnityEngine;
using UnityEngine.UI;

namespace ObserverScripts.UI.Game
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Actions.Controller actionsController;
        [SerializeField] private Builds.Controller buildsController;
        [SerializeField] private Character.Entity.Controller hostage;
        [SerializeField] private Button goToMarketButton;
        [SerializeField] private Market market;

        private void Awake()
        {
            actionsController.UpdateActionEvent += hostage.SetActionType;
            buildsController.UpdateTargetEvent += hostage.SetSubsriberTarget;
            goToMarketButton.onClick.AddListener(market.SendUpdateSubscribers);
        }
    }
}