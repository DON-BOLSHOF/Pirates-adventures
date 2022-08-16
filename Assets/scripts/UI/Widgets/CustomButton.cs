using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.Widgets
{
    public class CustomButton : Button
    {
        [SerializeField] private GameObject _normal;
        [SerializeField] private GameObject _pressed;

        protected override void DoStateTransition(SelectionState state, bool instance)
        {
            base.DoStateTransition(state, instance);

            _normal.SetActive(state != SelectionState.Pressed);
            _pressed.SetActive(state == SelectionState.Pressed);
        }
    }
}
