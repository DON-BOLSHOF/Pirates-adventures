using System;
using UnityEngine;

namespace PixelCrew.Effects
{
    public class InfiniteBG : MonoBehaviour
    {
        private float _length, _startX;
        [SerializeField] private Camera _camera;
        [SerializeField][Range(0,1f)] private float _paralaxEffect;

        private void Start()
        {
            _startX = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float temp = _camera.transform.position.x * (1 - _paralaxEffect);
            float dist = _camera.transform.position.x * _paralaxEffect;

            transform.position = new Vector3(_startX + dist, transform.position.y);

            if (temp > _startX + _length)
                _startX += _length;
            else if (temp < _startX - _length)
                _startX -= _length;
        }
    }
}