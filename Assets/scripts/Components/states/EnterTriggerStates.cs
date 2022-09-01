using PixelCrew.Components.states;
using UnityEngine;
namespace PixelCrew.Components
{
    public class EnterTriggerStates: MonoBehaviour
    {
        [SerializeField] private string _tagCollider = "Player";
        [SerializeField] private string _stateToClip;
        [SerializeField] private SpriteAnimationStates _action;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_tagCollider))
            {
                _action.SetClip(_stateToClip);
            }
        }
    }
}
