using UnityEngine;

namespace Assets.scripts.Utils
{
    public class AudioUtils
    {
        public const string SfxSourceTag = "SFXAudioSource";
        public static AudioSource FindSFXSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}
