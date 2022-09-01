using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverlapEvent;

        private readonly Collider2D[] _interactResult = new Collider2D[10];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius,
                          _interactResult,_mask);

            var overlaps = new List<GameObject>();
            for (int i = 0; i < size; i++)
            {
                var isInTag = _tags.Any(tag => _interactResult[i].CompareTag(tag));
                if(isInTag)
                    _onOverlapEvent?.Invoke(_interactResult[i].gameObject);
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject> { }

    }
}
