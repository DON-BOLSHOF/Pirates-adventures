using UnityEngine;

namespace Assets.scripts.Components.ColliderBased
{
    class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchingLayer;
        public bool IsTouchingLayer => _isTouchingLayer;

    }
}
