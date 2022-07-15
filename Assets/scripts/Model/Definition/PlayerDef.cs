﻿using UnityEngine;

namespace Assets.scripts.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]

    public class PlayerDef :ScriptableObject
    {
        [SerializeField] private int _inventorySize;

        public int InventorySize => _inventorySize;
    }
}