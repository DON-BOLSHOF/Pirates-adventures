using Assets.scripts.Model;
using System;

namespace Assets.scripts.Components.Stats
{
    class ExtraSpeed : StatsDecorator
    {
        public ExtraSpeed(IStatsProvider statsProvider) : base (statsProvider)
        {
            _duration = new Cooldown { cooldownTime = 5f };
            _duration.Reset();
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats();

            if (!_duration.IsReady)
                result.Agility *= 1.5f;

            return result;
        }
    }
}
