using System;

namespace PixelCrew.Components.Stats
{
    public class SpecializatonStats : IStatsProvider
    {
        private readonly Specialization _specialization;

        public SpecializatonStats(Specialization specialization)
        {
            _specialization = specialization;
        }

        public CreatureStats GetStats()
        {
            return GetSpecStats(_specialization);
        }

        private CreatureStats GetSpecStats(Specialization specialization)
        {
            switch (specialization)
            {
                case Specialization.Warrior:
                    return new CreatureStats()
                    {
                        Strength = 10,
                        Agility = 5,
                        Intellegence = 4
                    };
                case Specialization.Ranger:
                    return new CreatureStats()
                    {
                        Strength = 7,
                        Agility = 9,
                        Intellegence = 6
                    };
                default:
                    throw new NotImplementedException($"Specialization {specialization} dont implemented!");
            }
        }
    }

}
