using DefaultNamespace;
using Spawners;
using UnityEngine;

namespace UI.Play
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private Game.Controller gameScreen;
        [SerializeField] private Lose.Controller loseScreen;
        [SerializeField] private Win.Controller winScreen;
        [SerializeField] private Pause.Controller pauseScreen;
        [SerializeField] private StopController stopController;
        [SerializeField] private TimeStats timeStats;
        private RechangePanel _rechangePanel;

        private void Awake()
        {
            _rechangePanel = new RechangePanel();
            playerSpawner.SpawnPlayerEvent += gameScreen.SetPlayer;
            playerSpawner.SpawnPlayerEvent += container => container.HealView.OnDeathEvent += SetLoseScreen;
            pauseScreen.ContinueEvent += () => stopController.OnStopCallback(false);
            timeStats.TimeUpdateEvent += gameScreen.OnTimeChanged;
            timeStats.TimeIsOverEvent += SetWinScreen;
            stopController.Subscribe(UpdateStop);
            SetGameScreen();
        }

        private void UpdateStop(bool value)
        {
            if (value) SetPauseScreen();
            else SetGameScreen();
        }

        private void SetGameScreen()
        {
            _rechangePanel.SetPanel(gameScreen);
        }

        private void SetWinScreen()
        {
            _rechangePanel.SetPanel(winScreen);
        }

        private void SetLoseScreen()
        {
            _rechangePanel.SetPanel(loseScreen);
        }

        private void SetPauseScreen()
        {
            _rechangePanel.SetPanel(pauseScreen);
        }
    }
}