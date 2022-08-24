using UnityEngine;

namespace Assets.scripts.Components.Interaction
{
    class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject go)
        {
            var interactable = go.GetComponent<InteractableComponent>();
            if (interactable != null)
                interactable.Interact();
        }
    }
}
