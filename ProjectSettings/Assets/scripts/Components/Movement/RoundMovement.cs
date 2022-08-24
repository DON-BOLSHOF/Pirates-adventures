using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.Components.Movement
{
    class RoundMovement : MonoBehaviour
    {
        [SerializeField] private float _radious;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _count;

        private List<MovingItems> _items;
        private float _step;

        private void Awake()
        {
            InstantiatePrefabs(_count);

            _step = 360 / _count;
        }

        private void Start()
        {
            _items = new List<MovingItems>();

            UpdateContent();
        }

        private void UpdateContent()
        {
            int index = 0;
            foreach(var rb in this.GetComponentsInChildren<Rigidbody2D>())
            {
                _items.Add(new MovingItems { rigidbody = rb, itemAngle = _step * index });
                index++;
            }
        }

        private void Update()
        {
            var isAllDead = true;
            foreach(var item in _items)
            {
                if (item.rigidbody == null) continue;

                var position = transform.position + PositionToMove(item.itemAngle);

                item.rigidbody.MovePosition(position);

                isAllDead = false;
            }

            if (isAllDead)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
        }

        private Vector3 PositionToMove(float angle)
        {
            var angleRadians = angle * Mathf.PI / 180.0f;

            var x =_radious * Mathf.Cos(angleRadians + Time.time * _speed);
            var y =_radious * Mathf.Sin(angleRadians + Time.time * _speed);

            return new Vector3(x, y, 0);
        }

        private void InstantiatePrefabs(int count)
        {
            var step = 2 * Mathf.PI / _count;
            for (int i = 0; i < count; i++)
            {
                GameObject go = Instantiate(_prefab, new Vector2(_radious * Mathf.Cos(step*i), _radious * Mathf.Sin(step * i)), Quaternion.identity) as GameObject;
                go.transform.parent = transform;
            }
        }

        private class MovingItems
        {
            public float itemAngle;
            public Rigidbody2D rigidbody;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radious);
        }
#endif
    }
}
