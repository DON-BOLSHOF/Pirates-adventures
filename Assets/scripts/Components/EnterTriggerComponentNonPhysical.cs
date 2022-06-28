using System;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.scripts.Components
{
    public class EnterTriggerComponentNonPhysical : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_tag))
            {
                _action?.Invoke(collision.gameObject);
            }
        }

        [Serializable]
        class EnterEvent : UnityEvent<GameObject>
        { }
    }
}
