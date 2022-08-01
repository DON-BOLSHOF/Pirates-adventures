using Assets.scripts.Model;

namespace Assets.scripts.Components.Stats
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider _wrappedEntity;

        protected Cooldown _duration; 

        public StatsDecorator(IStatsProvider statsProvider)
        {
            _wrappedEntity = statsProvider;
        }

        public CreatureStats GetStats()
        {
            return GetInternalStats();
        }

        protected abstract CreatureStats GetInternalStats();
    }

}
