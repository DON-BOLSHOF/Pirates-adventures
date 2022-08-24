namespace Assets.scripts.Components.Stats
{
    public class SpeedBuff : StatsDecorator
    {
        public SpeedBuff(IStatsProvider statsProvider) : base(statsProvider)
        {
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats();
            result.Agility *= 2f;

            return result;
        }
    }

}
