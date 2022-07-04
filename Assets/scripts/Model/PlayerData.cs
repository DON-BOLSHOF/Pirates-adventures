using System;

namespace Assets.scripts.Model
{
    [Serializable]
    class PlayerData
    {
        public float Coins;
        public int Health = 10;
        public bool IsArmed;
        public int SwordCount = 3;

        public PlayerData(PlayerData _data)
        {
            Coins = _data.Coins;
            Health = _data.Health;
            IsArmed = _data.IsArmed;
            SwordCount = _data.SwordCount;
        }
    }
}
