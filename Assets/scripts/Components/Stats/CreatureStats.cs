namespace PixelCrew.Components.Stats
{
    public struct CreatureStats
    {
        public float Strength { get; set; }
        public float Agility { get; set; }
        public float Intellegence { get; set; }
        public static CreatureStats operator +(CreatureStats a, CreatureStats b)
        {
            return new CreatureStats()
            {
                Strength = a.Strength + b.Strength,
                Agility = a.Agility + b.Agility,
                Intellegence = a.Intellegence + b.Intellegence,            };
        }

        public static CreatureStats operator -(CreatureStats a, CreatureStats b)
        {
            return new CreatureStats()
            {
                Strength = a.Strength - b.Strength,
                Agility = a.Agility - b.Agility,
                Intellegence = a.Intellegence - b.Intellegence,            };
        }
        public static CreatureStats operator *(CreatureStats a, float b)
        {
            return new CreatureStats()
            {
                Strength = a.Strength * b,
                Agility = a.Agility * b,
                Intellegence = a.Intellegence * b,
            };
        }

        public override string ToString()
        {
            return $"Strength {Strength}\nAgility {Agility}\nIntelligence {Intellegence}";
        }
    }
}
