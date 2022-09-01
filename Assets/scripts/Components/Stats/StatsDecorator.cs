using PixelCrew.Model;

namespace PixelCrew.Components.Stats
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider _wrappedEntity;

        public Cooldown _duration; 

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
