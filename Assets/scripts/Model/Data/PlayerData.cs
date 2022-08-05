using Assets.scripts.Model.Data.Properties;
using System;
using UnityEngine;

namespace Assets.scripts.Model.Data
{
    [Serializable]
    class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public InventoryData Inventory => _inventory;

        public IntPersistantProperty Health = new IntPersistantProperty(); 

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
