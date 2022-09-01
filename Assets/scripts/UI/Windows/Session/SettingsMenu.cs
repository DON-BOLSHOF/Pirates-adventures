using System;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.Windows.Session
{
    class SettingsMenu : PauseSessionMenu
    {
        private Action _closeAction;

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
    }
}
