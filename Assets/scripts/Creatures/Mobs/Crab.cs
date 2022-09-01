using UnityEngine;
using System;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Stats;

namespace PixelCrew.Creatures.Mobs
{
    class Crab : Creature
    {
        [SerializeField] protected CheckCircleOverlap _screamRange;

        private static readonly int ScreamKey = Animator.StringToHash("OnScream");

        protected override IStatsProvider SetProvider()
        {
            return new SpecializatonStats(Specialization.Warrior);
        }

        public void HellScream()
        {
            Animator.SetTrigger(ScreamKey);
        }

        public void OnHellScream()
        {
            _screamRange.Check();
            Sounds.PlayClip("HellScream");
            _particles.Spawn("ScreamParticle");
            Debug.Log(Provider.GetStats().ToString());
        }

        public void Screaming(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            hero?.OnBuffed(new HellScream(hero.Provider));
        }
    }
}
