using Assets.scripts.Components.ColliderBased;
using System.Collections;
using UnityEngine;

namespace Assets.scripts.Creatures.Patrolling
{
    class PlatformPatrolling : Patrol
    {
        [SerializeField] private LayerCheck _groundCheck;

        private Creature _creature;
        private Vector2 _direction;

        private bool _isSwaped;

        private void Awake()
        {
            _creature = GetComponent<Creature>();

            _direction = new Vector2(1, 0);
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (!_groundCheck.IsTouchingLayer && !_isSwaped)
                {
                    _direction.x = -_direction.x;

                    _creature.SetDirection(Vector2.zero);

                    _isSwaped = true;

                    yield return new WaitForSeconds(0.5f);
                }

                if (_groundCheck.IsTouchingLayer && _isSwaped)
                    _isSwaped = false;

                _creature.SetDirection(_direction);

                yield return null;
            }
        }

    }
}
