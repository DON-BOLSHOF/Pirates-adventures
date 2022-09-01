using PixelCrew.Creatures;
using PixelCrew.Model.Definition;
using PixelCrew.Model.Definition.Repositories.Items;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    class InventoryAddComponent: MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if(hero != null)
            {
                hero.AddInInventory(_id, _count);
            }
        }
    }
}
