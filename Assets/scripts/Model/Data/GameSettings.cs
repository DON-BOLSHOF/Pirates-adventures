using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [CreateAssetMenu(menuName = "Defs/GameSettings", fileName = "GameSettings")]
    class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistantProperty _music;
        [SerializeField] private FloatPersistantProperty _sfx;

        public FloatPersistantProperty Music => _music;
        public FloatPersistantProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistantProperty(1, SoundSetting.Music.ToString());
            _sfx = new FloatPersistantProperty(1, SoundSetting.SFX.ToString());
        }

        private void OnValidate()
        {
            _music.Validate();
            _sfx.Validate();
        }
    }
        public enum SoundSetting
        {
            Music,
            SFX
        }
}
