using Assets.scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.scripts.Components.ColliderBased
{
    public class EnterTriggerComponentNonPhysical : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;

            _action?.Invoke(collision.gameObject);
        }

        [Serializable]
        class EnterEvent : UnityEvent<GameObject>
        { }
    }
}
