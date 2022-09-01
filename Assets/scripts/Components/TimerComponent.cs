using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    class TimerComponent : MonoBehaviour
    {
        [SerializeField] private Timer[] _timers;

        private Timer _currentTimer;

        public void SetTimer(string index)
        {
            _currentTimer = Array.Find(_timers, _timer => _timer.index == index);

            if(_currentTimer == null)
            {
                Debug.LogError("Не было найдено событие");
                return;
            }

            StartCoroutine(StartTimer(_currentTimer));
        }

        private IEnumerator StartTimer(Timer timer)
        {
            yield return new WaitForSeconds(timer.time);
            timer.onComplete?.Invoke();
        }
       
        [Serializable]
        private class Timer {
        public string index;
        public float time;
        public UnityEvent onComplete;
        }
    }
}
