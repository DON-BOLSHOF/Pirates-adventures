using Assets.scripts.Model;
using Assets.scripts.Model.Definition;
using Assets.scripts.UI.Widgets;
using System;
using UnityEngine;

namespace Assets.scripts.UI.HUD
{
    class HUDController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Health.OnChanged += OnHealthChanged;

            OnHealthChanged(_session.Data.Health.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        private void OnDestroy()
        {
            _session.Data.Health.OnChanged -= OnHealthChanged;
        }
    }
}
