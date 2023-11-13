using Spawners;
using Unit.Player;
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
        [SerializeField] private Observer restartObserver;
        private IObserverListenable _restartListenable;
        private IObserverCallbackable _restartCallbackable;
        private RechangePanel _rechangePanel;

        private void Awake()
        {
            _restartListenable = restartObserver;
            _restartCallbackable = restartObserver;
            _rechangePanel = new RechangePanel();
            gameScreen.ShowAbilityEvent += SetAbilityScreen;
            playerSpawner.SpawnPlayerEvent += PlayerSpawn;
            pauseScreen.ContinueEvent += () => stopController.OnStopCallback(false);
            timeStats.TimeUpdateEvent += gameScreen.OnTimeChanged;
            timeStats.TimeIsOverEvent += SetWinScreen;
            stopController.Subscribe(UpdateStop);
            _restartListenable.Subscribe(SetGameScreen);
            SetGameScreen();
        }

        private void Start()
        {
            winScreen.RestartEvent += _restartCallbackable.OnCallback;
            loseScreen.RestartEvent += _restartCallbackable.OnCallback;
        }

        private void PlayerSpawn(Container container)
        {
            gameScreen.SetPlayer(container);
            container.HealView.OnDeathEvent += SetLoseScreen;
            gameScreen.ShowAbilityEvent += value =>
            {
                if (value == false)
                {
                    container.ResetManaDelegate?.Invoke();
                }
            };
        }
        private void SetAbilityScreen(bool value)
        {
            if (value)
            {
                stopController.Unsubscribe(UpdateStop);
                stopController.OnStopCallback(true);
            }
            else
            {
                stopController.OnStopCallback(false);
                stopController.Subscribe(UpdateStop);
            }
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

        private void OnDestroy()
        {
            _restartListenable.Unsubscribe(SetGameScreen);
            stopController.Unsubscribe(UpdateStop);
            playerSpawner.SpawnPlayerEvent -= gameScreen.SetPlayer;
            winScreen.RestartEvent -= _restartCallbackable.OnCallback;
            loseScreen.RestartEvent -= _restartCallbackable.OnCallback;
            timeStats.TimeUpdateEvent -= gameScreen.OnTimeChanged;
            timeStats.TimeIsOverEvent -= SetWinScreen;
        }
    }
}