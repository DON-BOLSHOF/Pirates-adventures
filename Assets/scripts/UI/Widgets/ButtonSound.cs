using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelCrew.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] AudioClip _audioClip;

        private AudioSource _source;

        private void Start()
        {
            _source = AudioUtils.FindSFXSource();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _source.PlayOneShot(_audioClip);
        }
    }
}
