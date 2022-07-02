using UnityEngine;

namespace Assets.scripts.Components.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    class ProjectTile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private int _direction;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = transform.lossyScale.x > 0 ? 1 : -1;

            _rigidbody.AddForce(new Vector2(_direction * _speed, 0), ForceMode2D.Impulse);
        }
    }
}
