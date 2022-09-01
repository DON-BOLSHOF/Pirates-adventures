using UnityEngine;

namespace PixelCrew.UI.Windows.Session
{
    class PauseSessionMenu : AnimatedWindow
    {
        private float _defaultTimeScale;

        protected virtual void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}