using System;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;

        private void Awake()
        {
            _source = AudioUtils.FindSFXSource();
        }

        public void PlayClip(string id)
        {
            foreach(var sound in _sounds)
            {
                if (sound.Id != id) continue;

                _source.PlayOneShot(sound.Clip);
                break;
            }
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}
