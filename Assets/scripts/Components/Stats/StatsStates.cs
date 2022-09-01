using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCrew.Components.Stats
{
    class StatsStates : StatsDecorator
    {
        private StatsDecorator newStats;

        public StatsStates(IStatsProvider statsProvider, StatsDecorator statsDecorator) : base(statsProvider)
        {
            newStats = statsDecorator;
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats() + newStats.GetStats();

            return result;
        }
    }
}
