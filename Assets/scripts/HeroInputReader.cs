using Assets.scripts.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.scripts
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        public void OnHorizontalMovement(InputAction.CallbackContext context)
        {
            var _direction = context.ReadValue<Vector2>();
            _hero.SetDirection(_direction);
        }
        public void Interact(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }
        public void Attacking(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Attack();
            }
        }
    }
}