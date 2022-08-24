using Assets.scripts.Model;

namespace Assets.scripts.Components.Stats
{
    class HellScream : StatsDecorator
    {
        public HellScream(IStatsProvider statsProvider) : base(statsProvider)
        {
            _duration = new Cooldown { cooldownTime =5f};
            _duration.Reset();
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats();

            if(!_duration.IsReady)
                result.Agility *= 0.5f;

            return result;
        }
    }
}
