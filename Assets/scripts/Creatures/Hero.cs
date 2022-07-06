using UnityEngine;
using Assets.scripts.Components;
using Assets.scripts.Utils;
using UnityEditor.Animations;
using Assets.scripts.Model;
using Assets.scripts.Components.ColliderBased;
using System.Collections;

namespace Assets.scripts.Creatures
{
    class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactingCheck;

        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] private float  _slamDownVelocity = 3;

        [SerializeField] private Cooldown _cooldownThrowing;

        private Collider2D[] _interactResult = new Collider2D[1];
        private bool _allowDoubleJump;

        private GameSession _session;

        private bool _isOnWall;
        private float _defaultGravityScale;

        private int _swordCount =>  _session.Data.Inventory.Count("Sword");
        private int _coinCount => _session.Data.Inventory.Count("Coin");


        private static readonly int _isOnWallKey = Animator.StringToHash("Is-on-wall");

        protected override void Awake()
        {
            base.Awake();

            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            var _health = GetComponent<HealthComponent>();
            _health.SetHP(_session.Data.Health);

            _session.Data.Inventory.OnChange += OnInventoryChanged;

            UpgradeHeroWeapon();
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpgradeHeroWeapon();
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if(_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(_isOnWallKey, _isOnWall);
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChange -= OnInventoryChanged;
        }
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Health = currentHealth;
        }

        protected override float CalculateYVeclocity()
        {
            var _isJumpPressing = Direction.y > 0;

            if (IsOnGrounded || _isOnWall){_allowDoubleJump = true;}

            if(!_isJumpPressing && _isOnWall) { return 0f; }
            
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

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (_coinCount > 0)
                SpawnCoins();
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_coinCount, 5);
            _session.Data.Inventory.Remove("Coin",numCoinsToDispose);

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
            if (_swordCount <= 0) return;

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

        private void UpgradeHeroWeapon()
        {
            Animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _disarmed;
        }

        public void Throw()
        {
            if (_swordCount > 1 && _cooldownThrowing.IsReady)
            {
                _particles.Spawn("ThrowSword");
                _session.Data.Inventory.Remove("Sword", 1);
                _cooldownThrowing.Reset();
            }
        }

        public IEnumerator LongThrow()
        {

                for (int i = 0; i < 3 && _swordCount > 1; i++, _session.Data.Inventory.Remove("Sword", 1))
                {
                    _particles.Spawn("ThrowSword");
                    yield return new WaitForSeconds(0.2f);
                }
        }
    }
}

