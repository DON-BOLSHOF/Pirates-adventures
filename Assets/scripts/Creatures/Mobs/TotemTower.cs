using Assets.scripts.Components;
using Assets.scripts.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs
{
    class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAI> _traps;
        [SerializeField] private Cooldown _cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach(var trap in _traps)
            {
                trap.enabled = false;
                var hp = trap.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDead(trap));
            }
        }

        private void OnTrapDead(ShootingTrapAI trap)
        {
            var index = _traps.IndexOf(trap);
            _traps.Remove(trap);

            if (index < _currentTrap)
                _currentTrap--;
        }

        private void Update()
        {
            if(_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarger = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTarger)
            {
                if(_cooldown.IsReady)
                {
                    _traps[_currentTrap].RangeAttack();
                    _cooldown.Reset();
                    _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
            }
        }
    }
}
