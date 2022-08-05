using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.scripts.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] AudioClip _audioClip;

        private AudioSource _source;

        private void Start()
        {
            _source = GameObject.FindWithTag("SFXAudioSource").GetComponent<AudioSource>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _source.PlayOneShot(_audioClip);
        }
    }
}
