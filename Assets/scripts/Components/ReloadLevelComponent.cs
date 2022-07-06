using Assets.scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.Components
{
    class ReloadLevelComponent : MonoBehaviour
    {
        public void ReloadLevel()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
