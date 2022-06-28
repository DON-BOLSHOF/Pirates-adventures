using Assets.scripts.Creatures;
using UnityEngine;

namespace Assets.scripts.Components
{
    class AddCoinsComponent : MonoBehaviour
    {

        [SerializeField] private int _value;
        public void AddCoin(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddCoins(_value);
            }
        }
    }
}
