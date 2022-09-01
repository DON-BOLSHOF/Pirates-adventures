using UnityEngine;

namespace PixelCrew.Components.states
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimationStates : MonoBehaviour
    {
        [SerializeField] private State[] _states;
        [SerializeField] private int _frameRate;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private int _currentStateIndex;
        private float _nextFrameTime;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;
            _currentSpriteIndex = 0;
            _currentStateIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            _renderer.sprite = _states[_currentStateIndex].sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;

            if (_currentSpriteIndex >= _states[_currentStateIndex].sprites.Length-1)
            {
                if (_states[_currentStateIndex].loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _states[_currentStateIndex].onComplete?.Invoke();

                    if (_states[_currentStateIndex].allowsNext)
                    {
                        _currentStateIndex++;
                        _currentSpriteIndex = 0;
                    }
                }
            }
            else
            {
                _currentSpriteIndex++;
            }
        }

        public void SetClip(string _name)
        {
            bool _founded = false;

            for(int i = 0; i <_states.Length && !_founded; i++)
            {
                if(_states[i].name == _name)
                {
                    _currentStateIndex = i;
                    _currentSpriteIndex = 0;
                    _founded = true;
                }  
            }

            if (!_founded)
                Debug.Log("�� ���� ����� State � " + gameObject.name);
        }
    }
}