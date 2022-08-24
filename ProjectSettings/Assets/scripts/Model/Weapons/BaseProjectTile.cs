using UnityEngine;

namespace Assets.scripts.Model.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    class BaseProjectTile : MonoBehaviour
    {
        [SerializeField] protected float _speed;

        [SerializeField] protected bool _isInvertedX;

        protected Rigidbody2D Rigidbody;
        protected int Direction;

        protected virtual void Start()    
        {
            Rigidbody = GetComponent<Rigidbody2D>();

            var mod = _isInvertedX ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
        }
    }
}
