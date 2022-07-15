using Assets.scripts.Model;
using System;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHealth;
        [SerializeField] public UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int value)
        {
            _health += value;
            _onChange?.Invoke(_health);

            if (value < 0)
                _onDamage.Invoke();
            else if (value > 0)
                _onHealth.Invoke();

            if (_health <= 0)
                _onDie?.Invoke();

        }

        public void SetHP(int value)
        {
            _health = value;
        }

        private void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}
