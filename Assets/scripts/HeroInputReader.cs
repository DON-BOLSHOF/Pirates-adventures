using PixelCrew.Creatures;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static PixelCrew.Utils.EnumsUtils;

namespace PixelCrew
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
        public void Throwing(InputAction.CallbackContext context)
        {
            if (context.interaction is HoldInteraction && context.performed)
            {
                _hero.Throw(EnumsUtils.ThrowType.Long);
            }
            else if(context.interaction is PressInteraction)
            {
                _hero.Throw(EnumsUtils.ThrowType.Common);
            }
        }

        public void OnHelthPotion(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.OnPotion();
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
                _hero.NextItem();
        }
    }
}