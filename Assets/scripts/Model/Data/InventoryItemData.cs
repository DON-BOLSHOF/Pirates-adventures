using System;
using PixelCrew.Model.Definition;
using PixelCrew.Model.Definition.Repositories.Items;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}