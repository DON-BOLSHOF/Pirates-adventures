using Assets.scripts.Model;
using Assets.scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.UI.Windows.Settings
{
    class SettingsMenu : AnimatedWindow
    {
        private Action _closeAction;
        private float _defaultTimeScale;

        protected virtual void Start()
        {
            base.Start();

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void OnShowSettings()
        {
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnRestartLVL()
        {
            _closeAction = () => {
                var session = FindObjectOfType<GameSession>();
                session.LoadLastSave();

                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            };
            Close();
        }

        public void OnExit()
        {
            SceneManager.LoadScene("Main menu");

            var gameSession = FindObjectOfType<GameSession>();
            Destroy(gameSession.gameObject);
        }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}
