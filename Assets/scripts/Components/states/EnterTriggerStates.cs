using Assets.scripts.Components.states;
using UnityEngine;
namespace Assets.scripts.Components
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
