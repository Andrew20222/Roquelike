using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TimeStats : MonoBehaviour
    {
        [SerializeField] private TimeBar bar;
        [SerializeField] private float time;
        private Timer _timer;
        private void Start()
        {
            _timer = new Timer(this);
            bar.Init(_timer);
            _timer.Set(time);
            _timer.StartCountingTime();
        }

        public Timer GetTimer() => _timer;
    }
}