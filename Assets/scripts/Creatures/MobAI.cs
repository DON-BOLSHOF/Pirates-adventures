using Assets.scripts.Components;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Creatures;
using Assets.scripts.Creatures.Patrolling;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay;
        [SerializeField] private float _attackCoolDown;
        [SerializeField] private float _missHeroCooldown = 1f;

        private IEnumerator _current;
        private GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("Is-Dead");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        private bool _isDead;
        private Patrol _patrol;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead)
                return;

            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("ExclamationParticle");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirection();
            _creature.SetDirection(Vector3.zero);
            _creature.UpgradeSpriteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
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

            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirection();
            _creature.SetDirection(direction);
        }

        private Vector2 GetDirection()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector3.zero);

            if (_current != null)
                StopCoroutine(_current);

            _current = coroutine;
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(IsDeadKey, true);

            _creature.SetDirection(Vector3.zero);

            if (_current != null)
                StopCoroutine(_current);
        }
    }
}
