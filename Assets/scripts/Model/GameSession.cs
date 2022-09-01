using System.Collections.Generic;
using System.Linq;
using PixelCrew.Components.LVLManagment;
using PixelCrew.Model.Data;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    class GameSession: MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckPoint;
        public PlayerData Data => _data;

        private PlayerData _save;
        
        public QuickInventoryModel QuickInventory { get; private set; }

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private readonly List<string> _checkPoints = new List<string>();

        private void Awake()
        {
            var existGameSession = GetSession();
            if (existGameSession != null)
            {
                existGameSession.SetChecked(_defaultCheckPoint);
                existGameSession.StartSession();
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                InitModels();
                Save();
                StartSession();
            }
        }

        private void StartSession()
        {
            SetChecked(_defaultCheckPoint);

            LoadHUD();
            SpawnHero();
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

        private void SpawnHero()
        {
            var checkPoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkPoints.Last();

            foreach (var checkPoint in checkPoints)
            {
                if (checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    return;
                }
            }
        }

        private GameSession GetSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return gameSession;
            }

            return null;
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

        internal bool IsChecked(string id)
        {
            return _checkPoints.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (!IsChecked(id))
            {
                Save();
                _checkPoints.Add(id);
            }
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
