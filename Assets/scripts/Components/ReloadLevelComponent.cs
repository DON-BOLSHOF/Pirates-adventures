using Assets.scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.Components
{
    class ReloadLevelComponent : MonoBehaviour
    {
        private Scene _scene;
        private GameSession _session;
        private PlayerData _onSpawnData;

        private void Start()
        {
            _scene = SceneManager.GetActiveScene();
            _session = FindObjectOfType<GameSession>();
            _onSpawnData = new PlayerData(_session.Data);
        }

        public void ReloadLevel()
        {
            _session.Data.Coins = _onSpawnData.Coins;
            _session.Data.Health = _onSpawnData.Health;
            _session.Data.IsArmed = _onSpawnData.IsArmed;

            SceneManager.LoadScene(_scene.name);
        }
    }
}
