using UnityEngine;
using Assets.scripts.Components;
using Assets.scripts.Utils;
using UnityEditor.Animations;
using Assets.scripts.Model;
using Assets.scripts.Components.ColliderBased;
using System.Collections;
using Assets.scripts.Components.Stats;
using Assets.scripts.Model.Definition;
using static Assets.scripts.Utils.EnumsUtils;
using System;

namespace Assets.scripts.Creatures
{
    class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactingCheck;

        [SerializeField] private ColliderCheck _wallCheck;

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Header("Sword Throw")]
        [SerializeField] private float _throwDelay = 0.2f;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        [SerializeField] private float _slamDownVelocity = 3;

        [SerializeField] private Cooldown _cooldownThrowing;
        [SerializeField] private SpawnComponent _throwSpawner;

        private bool _allowDoubleJump;

        private GameSession _session;

        private HealthComponent _health;

        private bool _isOnWall;
        private float _defaultGravityScale;

        private const string SwordId = "Sword";

        private int _swordCount => _session.Data.Inventory.Count(SwordId);
        private int _coinCount => _session.Data.Inventory.Count("Coin");

        private static readonly int _isOnWallKey = Animator.StringToHash("Is-on-wall");

        private string SelectedId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow {
            get
            {
                if (SelectedId == SwordId)
                    return _swordCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _defaultGravityScale = Rigidbody.gravityScale;
        }

        internal void NextItem()
        {
            _session.QuickInventory.SetItemNext();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _health = GetComponent<HealthComponent>();
            _health.SetHP(_session.Data.Health.Value);

            _session.Data.Inventory.OnChange += OnInventoryChanged;
            _health.OnChange.AddListener(OnHealthChanged);

            UpgradeHeroWeapon();
        }

        protected override IStatsProvider SetProvider()
        {
            IStatsProvider mHero = new SpecializatonStats(Specialization.Warrior);
            mHero = new SpeedBuff(mHero);
            mHero = new SpeedBuff(mHero);

            return mHero;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == SwordId)
                UpgradeHeroWeapon();
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
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
            _health.OnChange.RemoveAllListeners();
        }
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Health.Value = currentHealth;
        }

        protected override float CalculateYVeclocity()
        {
            var _isJumpPressing = Direction.y > 0;

            if (IsOnGrounded || _isOnWall) { _allowDoubleJump = true; }

            if (!_isJumpPressing && _isOnWall) { return 0f; }

            return base.CalculateYVeclocity();
        }

        protected override float Jump(float yVelocity)
        {
            if (!IsOnGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;

                DoJumpVfx();
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
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

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
        public  void OnPotion()
        {
            if (!SelectedId.Contains("Potion"))
                return;

            var potionCount = _session.Data.Inventory.Count(SelectedId);
            if (potionCount > 0)
            {
                switch (SelectedId)
                {
                    case "HealthPotion":
                        _health.ModifyHealth(3);
                        break;
                    case "BigHealthPotion":
                        _health.ModifyHealth(5);
                        break;
                    case "SpeedPotion":
                        OnBuffed(new ExtraSpeed(Provider));
                        break;
                    default:
                        throw new ArgumentException("Invalid type of Potion");
                }

                _session.Data.Inventory.Remove(SelectedId, 1);
                _particles.Spawn("PosionParticle");
                Sounds.PlayClip("PosionUsing");
            }
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

                if (contact.relativeVelocity.y >= DamageVelocity)
                {
                    GetComponent<HealthComponent>().ModifyHealth(-1);
                }
            }
        }

        private void UpgradeHeroWeapon()
        {
            Animator.runtimeAnimatorController = _swordCount > 0 ? _armed : _disarmed;
        }

        public void OnCommonThrow(string throwableId)
        {
                OnThrowing();
                _throwSpawner.Spawn();
                _session.Data.Inventory.Remove(throwableId, 1);
        }

        public IEnumerator LongThrow(string throwableId)
        {
                for (int i = 0; i < 3 && ((throwableId == SwordId && _swordCount > 1) || (throwableId != SwordId)); i++, _session.Data.Inventory.Remove(throwableId, 1))
                {
                    OnThrowing();
                    _throwSpawner.Spawn();
                    yield return new WaitForSeconds(_throwDelay);
                }
        }

        public void Throw(ThrowType throwType)
        {
            if (CanThrow && _cooldownThrowing.IsReady)
            {
                var throwableId = _session.QuickInventory.SelectedItem.Id;
                var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
                _throwSpawner.SetPrefab(throwableDef.ProjectTile);

                switch (throwType)
                {
                    case ThrowType.Common:
                        OnCommonThrow(throwableId);
                        break;
                    case ThrowType.Long:
                        StartCoroutine(LongThrow(throwableId));
                        break;
                    default:
                        throw new ArgumentException("Invalid type of enum");
                }
            }
        }

        public void OnThrowing()
        {
            Sounds.PlayClip("Range");
            _cooldownThrowing.Reset();
        }
    }
}

