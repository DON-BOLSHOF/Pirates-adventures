using UnityEngine;

namespace PixelCrew.Effects
{
    public class ParalaxEffect : MonoBehaviour
    {
        [SerializeField][Range(0,1f)] private float _effectValue;
        [SerializeField] private Transform _followTarget;

        private float _xStart;
        
        private void Start()
        {
            _xStart = transform.position.x;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = _followTarget.position.x * _effectValue;
            transform.position = new Vector3(_xStart + deltaX, currentPosition.y, currentPosition.z);
        }
    }
}