using System.Collections;
using UnityEngine;

namespace Assets.scripts.Components
{
    class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime =1;


        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            yield return SetAlpha(sprite, 0);

            target.SetActive(false);
            yield return MoveAnimation(target);
            target.SetActive(true);

            yield return SetAlpha(sprite, 1);
        }

        private IEnumerator MoveAnimation(GameObject target) {
            var moveTime = 0f;
            while (moveTime < _moveTime)
            {
                moveTime += Time.deltaTime;
                target.transform.position = Vector3.Lerp(target.transform.position, _destTransform.position, moveTime / _moveTime);

                yield return null;
            }
        }
        

        private IEnumerator SetAlpha(SpriteRenderer sprite, float destAlpha)
        {
            var alphaTime = 0f;
            var spriteAlpha = sprite.color.a;
            while (alphaTime < _alphaTime)
            {
                alphaTime += Time.deltaTime;
                var tempAlpha = Mathf.Lerp(spriteAlpha, destAlpha, alphaTime / _alphaTime);
                var color = sprite.color;
                color.a = tempAlpha;
                sprite.color = color;

                yield return null;
            }
        }
    }
}
