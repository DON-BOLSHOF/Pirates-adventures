using Assets.scripts.Utils;
using System;
using UnityEngine;
using static Assets.scripts.Components.Audio.PlaySoundsComponent;

namespace Assets.scripts.Components.Audio
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
