using Assets.scripts.Creatures;
using UnityEngine;

namespace Assets.scripts.Components
{
    class ArmHeroComponent: MonoBehaviour
    {
        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if(hero != null)
            {
                hero.ArmHero();
            }
        }
    }
}
