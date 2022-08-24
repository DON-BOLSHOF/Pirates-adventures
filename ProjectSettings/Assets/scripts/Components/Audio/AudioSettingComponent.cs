using Assets.scripts.Model.Data;
using Assets.scripts.Model.Data.Properties;
using System;
using UnityEngine;

namespace Assets.scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    class AudioSettingComponent : MonoBehaviour
    {
        [SerializeField] private SoundSetting _mode;
        private AudioSource _source;
        private FloatPersistantProperty _model;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();    
        }

        private void Start()
        {
            _model = FindProperty();
            _model.OnChanged += OnSoundSettingsChanged;
            OnSoundSettingsChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingsChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistantProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSetting.Music:
                    return GameSettings.I.Music;
                case SoundSetting.SFX:
                    return GameSettings.I.Sfx;
            }

            throw new ArgumentException("Undefiend mode");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingsChanged;
        }
    }
}
