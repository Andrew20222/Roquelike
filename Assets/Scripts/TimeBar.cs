using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimeBar : Bar
    {
        private Timer _timer;

        public void Init(Timer timer)
        {
            _timer = timer;
            _timer.HasBeenUpdated += OnValueChanged;
        }

        private void OnDisable()
        {
            if(_timer ==null) return;
            _timer.HasBeenUpdated -= OnValueChanged;
        }
    }
}