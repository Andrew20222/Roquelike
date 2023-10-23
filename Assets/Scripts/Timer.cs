using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Timer
    {
        public event Action<float> HasBeenUpdated;
        public event Action TimeIsOver;
        private float _time;
        private float _remainigTime;
        private IEnumerator _countdown;
        private MonoBehaviour _context;

        public Timer(MonoBehaviour context) => _context = context;

        public void Set(float time)
        {
            _time = time;
            _remainigTime = _time;
        }

        public void StartCountingTime()
        {
            if (_countdown != null)
                _context.StartCoroutine(_countdown);
        }

        private IEnumerator Countdown()
        {
            while (_remainigTime >= 0)
            {
                _remainigTime -= Time.deltaTime;
                HasBeenUpdated?.Invoke(_remainigTime/_time);
                yield return null;
            }
            
            TimeIsOver?.Invoke();
        }

    }
}