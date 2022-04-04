using System;
using UnityEngine;

namespace Assets.Code.Global
{
    public class GameStateManager : MonoBehaviour
    {
        public event EventHandler<OnTimeChangedEventArg> OnTimeChangedEvent;

        public class OnTimeChangedEventArg
        {
            public float Time;
        }

        [SerializeField] private float _startLifeTime;
        private float _currentTime;
        private int _enemiesDefeated;

        public void CountKill()
        {
            _enemiesDefeated++;
        }

        public void ResetTimer(float timeForKill)
        {
            _currentTime = _startLifeTime;
            OnTimeChangedEvent?.Invoke(this, new OnTimeChangedEventArg { Time = _currentTime });
        }

        private void Start()
        {
            _currentTime = _startLifeTime;
        }

        private void Update()
        {
            Tick();
        }

        private void Tick()
        {
            if (_currentTime > 0f)
            {
                _currentTime -= Time.deltaTime;
                OnTimeChangedEvent?.Invoke(this, new OnTimeChangedEventArg { Time = _currentTime });
            }
        }
    }
}
