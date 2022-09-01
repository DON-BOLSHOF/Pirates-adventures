using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Definition;
using PixelCrew.Model.Definition.Repositories.Items;
using UnityEngine;

namespace PixelCrew.Model.Data
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

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                AddToStack(id, value);
            }
            else
            {
                AddNonStack(id, value);
            }
            OnChange?.Invoke(id, Count(id));
        }

        private void AddToStack(string id, int value)
        {
            var isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;

            var item = GetItem(id);
            if (item == null)
            {
                if (isFull) return;
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
        }
        
        private void AddNonStack(string id, int value)
        {
            var itemLasts = DefsFacade.I.Player.InventorySize - _inventory.Count;
            value = Mathf.Min(itemLasts, value);

            for (int i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) { Value = 1 };
                _inventory.Add(item);
            }
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                RemoveFromStack(id, value);
            }
            else
            {
                RemoveNonStack(id, value);
            }
            OnChange?.Invoke(id, Count(id));
        }

        private void RemoveFromStack(string id, int value) {
            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;
            if (item.Value <= 0)
                _inventory.Remove(item);
        }
        
        private void RemoveNonStack(string id, int value) {
            for (int i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if (item == null) return;

                _inventory.Remove(item);
            }
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

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var retValues = new List<InventoryItemData>();

            foreach(var item in _inventory)
            {
                var itemDef = DefsFacade.I.Items.Get(item.Id);
                var isAllRequirementsMet = tags.All(x => itemDef.HasTag(x));
                if (isAllRequirementsMet)
                    retValues.Add(item);
            }

            return retValues.ToArray();
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
