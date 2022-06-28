using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.Components
{
    class ExitLVLComponent :MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
