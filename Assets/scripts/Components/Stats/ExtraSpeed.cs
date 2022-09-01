using System;
using PixelCrew.Model;

namespace PixelCrew.Components.Stats
{
    class ExtraSpeed : StatsDecorator
    {
        public float _value;
        
        public ExtraSpeed(IStatsProvider statsProvider, float time, float value) : base (statsProvider)
        {
            _value = value;
            
            _duration = new Cooldown { cooldownTime = time };
            _duration.Reset();
        }

        protected override CreatureStats GetInternalStats()
        {
            var result = _wrappedEntity.GetStats();

            if (!_duration.IsReady)
                result.Agility *= _value;

            return result;
        }
    }
}
