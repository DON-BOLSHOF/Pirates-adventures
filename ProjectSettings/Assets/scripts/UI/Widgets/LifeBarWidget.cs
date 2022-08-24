using Assets.scripts.Components;
using Assets.scripts.Utils.Disposables;
using UnityEngine;

namespace Assets.scripts.UI.Widgets
{
    class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHealth;

        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHealth = _hp.Health;

            _trash.Retain(_hp.OnChange.Subscribe(OnHpChanged));
            _trash.Retain(_hp.OnDie.Subscribe(OnDied));
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var barValue =(float) hp / _maxHealth;
            _lifeBar.SetProgress(barValue);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
