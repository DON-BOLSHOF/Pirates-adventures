using UnityEngine;
using UnityEngine.Events;
using Assets.scripts.Components;
using Assets.scripts.Utils;
using UnityEditor.Animations;
using Assets.scripts.Model;

namespace Assets.scripts.Creatures
{
    class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactingCheck;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] private float  _slamDownVelocity = 3;

        [SerializeField] private Cooldown _cooldownThrowing;

        private Collider2D[] _interactResult = new Collider2D[1];
        private bool _allowDoubleJump;

        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();


        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;

            var _health = GetComponent<HealthComponent>();
            _health.SetHP(_session.Data.Health);
        }

        protected override void Update()
        {
            base.Update();

        }
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Health = currentHealth;
        }

        protected override float CalculateYVeclocity()
        {
            var _isJumpPressing = Direction.y > 0;

            if (IsOnGrounded){_allowDoubleJump = true;}
            
            return base.CalculateYVeclocity();
        }

        protected override float Jump(float yVelocity)
        {
         if (!IsOnGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;

                _particles.Spawn("JumpParticle");
                return JumpSpeed;
            }

            return base.Jump(yVelocity);
        }

        public void AddCoins(float value)
        {
            _session.Data.Coins += value;
        }

        public override void TakeDamage()
        {
            base.TakeDamage();

            if (_session.Data.Coins > 0)
                SpawnCoins();
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _interactingCheck.Check();
        }

        public override void Attack()
        {
            if (!_session.Data.IsArmed) return;

            base.Attack();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(GroundLayer))
            {
                var contact = collision.contacts[0];
                if (contact.relativeVelocity.y >= _slamDownVelocity)
                {
                    _particles.Spawn("FallParticle");
                }

                if(contact.relativeVelocity.y >= DamageVelocity)
                {
                    GetComponent<HealthComponent>().ModifyHealth(-1);
                }
            }
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            Animator.runtimeAnimatorController = _armed;
        }

        public void Throw()
        {
            if (_cooldownThrowing.IsReady)
            {
                _particles.Spawn("ThrowSword");
                _cooldownThrowing.Reset();
            }
        }
    }
}

