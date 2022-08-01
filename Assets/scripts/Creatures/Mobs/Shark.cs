using Assets.scripts.Components.Stats;

namespace Assets.scripts.Creatures.Mobs
{
    class Shark : Creature
    {
        protected override IStatsProvider SetProvider()
        {
            return new SpecializatonStats(Specialization.Warrior);
        }
    }
}
