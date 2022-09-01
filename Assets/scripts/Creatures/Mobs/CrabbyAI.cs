using PixelCrew.Components.Stats;
using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Scripts.Creatures.Mobs;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    class CrabbyAI : MobAI
    {
        [SerializeField] private float _screamCoolDown;
        [SerializeField] private ColliderCheck _canScream;

        private bool _hasScreamed;

        private Crab _crab;

        private void Awake()
        {
            base.Awake();

            _crab = (Crab)_creature;

            _hasScreamed = false;
        }

        protected override IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canScream.IsTouchingLayer && !_hasScreamed)
                {
                    _hasScreamed = true;
                    StartState(Scream());
                }
                else if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            _creature.SetDirection(Vector3.zero);
            _particles.Spawn("MissParticle");
            yield return new WaitForSeconds(_missHeroCooldown);
            _hasScreamed = false;

            StartState(_patrol.DoPatrol());
        }

        protected IEnumerator Scream()
        {
            _crab.HellScream();
            yield return new WaitForSeconds(_screamCoolDown);
            StartState(GoToHero());
        }
    }
}
