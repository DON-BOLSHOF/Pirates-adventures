using Assets.scripts.Model;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.UI.Settings
{
    class SettingsMenu : AnimatedWindow
    {
        private Action _closeAction;

        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
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
        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke();
            base.OnCloseAnimationComplete();
        }
    }
}
