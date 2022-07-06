using Assets.scripts.Creatures;
using Assets.scripts.Model.Definition;
using UnityEngine;

namespace Assets.scripts.Components.Collectables
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
