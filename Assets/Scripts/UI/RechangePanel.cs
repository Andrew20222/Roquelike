namespace UI
{
    public class RechangePanel
    {
        private IHidenable _currentPanel;

        public void SetPanel(IHidenable newPanel)
        {
            if (_currentPanel != null) _currentPanel.Hide();
            _currentPanel = newPanel;
            _currentPanel.Show();
        }
    }
}