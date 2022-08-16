using Assets.scripts.Model.Data.Properties;
using Assets.scripts.Model.Definition;
using Assets.scripts.Utils.Disposables;
using System;
using UnityEngine;

namespace Assets.scripts.Model.Data
{
    class QuickInventoryModel
    {
        private readonly PlayerData _data;

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntPersistantProperty SelectedIndex = new IntPersistantProperty();

        public event Action OnChanged;

        public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = data.Inventory.GetAll(ItemTag.Usable);
            _data.Inventory.OnChange += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = _data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }

        internal void SetItemNext()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value +1, Inventory.Length);
        }
    }
}
