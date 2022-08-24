using Assets.scripts.Model.Data;
using Assets.scripts.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts.Model
{
    class GameSession: MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        private PlayerData _save;
        
        public QuickInventoryModel QuickInventory { get; private set; }

        private CompositeDisposable _trash = new CompositeDisposable();

        private void Awake()
        {
            LoadHUD();

            if (IsSessionExit())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                InitModels();
                Save();
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);
        }

        private void LoadHUD()
        {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gamesession in sessions)
            {
                if (gamesession != this)
                    return true;
            }

            return false;
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

            _trash.Dispose();
            InitModels();
        }
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
