using System;
using PixelCrew.Utils;
using UnityEngine;
using static PixelCrew.Components.Audio.PlaySoundsComponent;

namespace PixelCrew.Components.Audio
{
    class PlaySFXSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private AudioSource _source;

        private void Awake()
        {
            _source = AudioUtils.FindSFXSource();
        }

        public void Play()
        {
            _source.PlayOneShot(_clip);
        }

    }
}
