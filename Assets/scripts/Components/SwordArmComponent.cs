using Assets.scripts.Creatures;
using UnityEngine;

namespace Assets.scripts.Components
{
    class SwordArmComponent : MonoBehaviour
    {

        [SerializeField] private int _value;
        public void AddSword(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddSwords(_value);
            }
        }
    }
}
