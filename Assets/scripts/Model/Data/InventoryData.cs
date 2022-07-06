using Assets.scripts.Model.Definition;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.Model.Data
{
    [Serializable]
    class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChange(string id, int value);

        public OnInventoryChange OnChange;
    
        public void Add(string id, int value)
        {
            if (value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);

            if(item == null)
            {
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
            OnChange?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;
            if (item.Value <= 0)
                _inventory.Remove(item);
            OnChange?.Invoke(id, Count(id));
        }

        private InventoryItemData GetItem(string id)
        {
            foreach(var itemData in _inventory)
            {
                if(itemData.Id == id)
                {
                    return itemData;
                }
            }

            return null;
        }

        public int Count(string id)
        {
            var count = 0;
            foreach(var item in _inventory)
            {
                if (item.Id == id)
                    count += item.Value;
            }

            return count;
        }
    }
}
