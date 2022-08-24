using Assets.scripts.Components.ColliderBased;
using System.Collections;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs.Patrolling
{
    class PlatformPatrolling : Patrol
    {
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private LayerCheck _obstacklesCheck;

        private Creature _creature;
        private Vector2 _direction;

        private void Awake()
        {
            _creature = GetComponent<Creature>();

            _direction = new Vector2(1, 0);
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (_groundCheck.IsTouchingLayer && !_obstacklesCheck.IsTouchingLayer)
                {
                    _creature.SetDirection(_direction);
                }
                else
                {
                    _direction = -_direction;
                    _creature.SetDirection(_direction);
                }

            yield return null;
            }
        }
    }
}
