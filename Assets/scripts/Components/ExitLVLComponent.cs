using Assets.scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.Components
{
    class ExitLVLComponent :MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();

            SceneManager.LoadScene(_sceneName);
        }
    }
}
