﻿using PixelCrew.Components.Stats;

namespace PixelCrew.Creatures.Mobs
{
    class Shark : Creature
    {
        protected override IStatsProvider SetProvider()
        {
            return new SpecializatonStats(Specialization.Warrior);
        }
    }
}
