using Assets.scripts.Model;
using Assets.scripts.Model.Data;
using Assets.scripts.UI.Widgets;
using Assets.scripts.Utils.Disposables;
using UnityEngine;

namespace Assets.scripts.UI.HUD.QuickInventory
{
    class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;

        private DataGroup<InventoryItemData, InventoryItemWidget>  _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_prefab, _container);

            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));

            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInventory.Inventory;
            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
