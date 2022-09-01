namespace PixelCrew.Components.Stats
{ 
    public class Weakness : StatsDecorator
    {
        public Weakness(IStatsProvider statsProvider) : base(statsProvider)
        {
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats();
            result.Strength *= 0.9f;
            result.Agility *= 0.9f;

            return result;
        }
    }

}
