using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class PortraitItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _portrait;
        [SerializeField] private Sprite _baseSprite;
        public Image Portrait => _portrait;

        private Coroutine _transformingRoutine;
        private bool _isReversed;

        private GameObject _parent;

        private void Start()
        {
            _parent = _portrait.transform.parent.gameObject;
        }

        public void SetPortrait(Sprite portrait, bool isReversed)
        {
            _portrait.sprite = portrait;
            _isReversed = isReversed;

            var localScale = gameObject.transform.localScale;
            localScale.x = _isReversed ? localScale.x * -1 : localScale.x;
            gameObject.transform.localScale = localScale;
        }

        public void WidePortrait()
        {
            _transformingRoutine = StartCoroutine(Widding(_parent));
        }

        private IEnumerator Widding(GameObject parent)
        {
            for (float i = 1.0f; i < 1.4f; i += 0.1f)
            {
                if (!_isReversed)
                    parent.transform.localScale += new Vector3(0.1f, 0.1f, 1);
                else
                {
                    var scale = parent.transform.localScale;
                    scale = new Vector3(scale.x - 0.1f, scale.y + 0.1f, 1);
                    parent.transform.localScale = scale;
                }

                yield return new WaitForSeconds(0.1f);
            }

            _transformingRoutine = null;
        }

        public void ConstrictPortrait()
        {
            var parentScale = _portrait.transform.parent.gameObject;
            _transformingRoutine = StartCoroutine(Constricting(parentScale));
        }

        private IEnumerator Constricting(GameObject parent)
        {
            for (float i = 1.4f; i >= 1.0f; i -= 0.1f)
            {
                if (!_isReversed)
                    parent.transform.localScale -= new Vector3(0.1f, 0.1f, 1);
                else
                {
                    var scale = parent.transform.localScale;
                    scale = new Vector3(scale.x + 0.1f, scale.y - 0.1f, 1);
                    parent.transform.localScale = scale;
                }

                yield return new WaitForSeconds(0.1f);
            }

            _transformingRoutine = null;
        }

        public void NormalizedScale()
        {
            _parent.transform.localScale = new Vector3(1, 1, 1);
        }

        public void OnDestroy()
        {
            _transformingRoutine = null;
        }

        public void SetBaseSprite()
        {
            _portrait.sprite = _baseSprite;
        }
    }
}
