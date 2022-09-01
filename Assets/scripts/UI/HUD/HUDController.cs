using PixelCrew.Model;
using PixelCrew.Model.Definition;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.UI.HUD
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
        public void OnShowSessionMenu()
        {
            WindowUtils.CreateWindow("UI/SessionMenu");
        }

        private void OnDestroy()
        {
            _session.Data.Health.OnChanged -= OnHealthChanged;
        }
    }
}
