using UnityEngine;

namespace Assets.scripts.Components.ColliderBased
{
    class LineCastCheck : LayerCheck
    {
        [SerializeField] private Transform _target;

        private readonly RaycastHit2D[] _result = new RaycastHit2D[1];

        private void Update()
        {
            _isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, _layer) > 0;
        }


#if UNITY_EDITOR
        private void OnDrawGismosSelected()
        {
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }

#endif
    }
}
