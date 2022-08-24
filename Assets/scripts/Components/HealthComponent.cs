using System;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] public UnityEvent OnDie;
        [SerializeField] public HealthChangeEvent OnChange;

        public int Health => _health;

        public void ModifyHealth(int value)
        {
            _health += value;
            OnChange?.Invoke(_health);

            if (value < 0)
                _onDamage.Invoke();
            else if (value > 0)
                _onHeal.Invoke();

            if (_health <= 0)
                OnDie?.Invoke();

        }

        public void SetHP(int value)
        {
            _health = value;
        }

        private void OnDestroy()
        {
            OnDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
        }
    }
}
