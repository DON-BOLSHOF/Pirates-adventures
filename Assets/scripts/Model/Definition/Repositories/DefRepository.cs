using UnityEngine;

namespace PixelCrew.Model.Definition.Repository
{
    public class DefRepository<TDefType>: ScriptableObject where TDefType:IHaveId
    {
        [SerializeField] protected TDefType[] _collections;

        public TDefType Get(string id)
        {
            if (id == null)
                return default;
                
            foreach (var itemDef in _collections)
            {
                if (itemDef.Id == id)
                {
                    return itemDef;
                }
            }

            return default;
        }
    }
}