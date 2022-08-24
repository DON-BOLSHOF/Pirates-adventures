using Assets.scripts.Components;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Creatures;
using Assets.scripts.Creatures.Mobs.Patrolling;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creatures.Mobs
{
    class MobAI : MonoBehaviour
    {
        [SerializeField] protected ColliderCheck _vision;
        [SerializeField] protected ColliderCheck _canAttack;

        [SerializeField] private float _alarmDelay;
        [SerializeField] private float _attackCoolDown;
        [SerializeField] protected float _missHeroCooldown = 1f;
        [SerializeField] private float _threshold;

        private IEnumerator _current;
        protected GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("Is-Dead");

        protected SpawnListComponent _particles;
        protected Creature _creature;
        private Animator _animator;
        protected bool _isDead;
        protected Patrol _patrol;

        protected void Awake()
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

        public virtual void OnHeroInVision(GameObject go)
        {
            if (_isDead)
                return;

            _target = go;

            StartState(AgroToHero());
        }

        protected IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("ExclamationParticle");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        protected void LookAtHero()
        {
            var direction = GetDirection();
            _creature.SetDirection(Vector3.zero);
            _creature.UpgradeSpriteDirection(direction);
        }

        protected virtual IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                    if (horizontalDelta >= _threshold)
                        SetDirectionToTarget();
                    else
                        _creature.SetDirection(Vector2.zero);
                }
                yield return null;
            }

            _creature.SetDirection(Vector3.zero);
            _particles.Spawn("MissParticle");
            yield return new WaitForSeconds(_missHeroCooldown);

            StartState(_patrol.DoPatrol());
        }

        protected IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }

            StartState(GoToHero());
        }

        protected void SetDirectionToTarget()
        {
            var direction = GetDirection();
            _creature.SetDirection(direction);
        }

        protected Vector2 GetDirection()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        protected void StartState(IEnumerator coroutine)
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
