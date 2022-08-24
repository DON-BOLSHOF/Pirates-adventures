using Assets.scripts.Components;
using Assets.scripts.Components.Audio;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Components.Stats;
using Assets.scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Assets.scripts.Creatures
{
    abstract class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] protected float JumpSpeed;
        [SerializeField] protected float DamageVelocity = 1;

        [SerializeField] private bool inversed;

        [Header("Checkers")]
        [SerializeField] private float _groundCheckRadious = 0;
        [SerializeField] protected LayerMask GroundLayer;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] protected CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        public IStatsProvider Provider { get; set; }

        protected Animator Animator;

        protected PlaySoundsComponent Sounds;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected float _speed => Provider.GetStats().Agility * 0.15f;

        protected bool IsOnGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("Is-Ground");
        private static readonly int Vertical_velocity = Animator.StringToHash("Vertical-Velocity");
        private static readonly int IsRunningKey = Animator.StringToHash("Is-Running");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();

            Provider = SetProvider();
        }

        protected abstract IStatsProvider SetProvider();

        public void OnBuffed(IStatsProvider stats)
        {
            Provider = stats;
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsOnGrounded = IsGrounded();
        }
        private bool IsGrounded()
        {
            var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadious, Vector2.down, 0, GroundLayer);
            return hit.collider != null;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVeclocity();
            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsGroundKey, IsOnGrounded);
            Animator.SetFloat(Vertical_velocity, Rigidbody.velocity.y);
            Animator.SetBool(IsRunningKey, Direction.x != 0);

            UpgradeSpriteDirection(new Vector3(xVelocity, yVelocity, 0));
        }


        protected virtual float CalculateYVeclocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var _isJumpPressing = Direction.y > 0;

            if (_isJumpPressing)
            {
                _isJumping = true;

                var IsFalling = Rigidbody.velocity.y <= 0.001f;

                yVelocity = IsFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            yVelocity = Jump(yVelocity);

            return yVelocity;
        }

        protected virtual float Jump(float yVelocity)
        {
            if (IsOnGrounded)
            {
                yVelocity += JumpSpeed;
                DoJumpVfx();
            }

            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            Sounds.PlayClip("Jump");
        }

        public void UpgradeSpriteDirection(Vector3 direction)
        {

            var multiplier = inversed ? -1.5f : 1.5f;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1.5f, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f * multiplier, 1.5f, 1);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = IsOnGrounded ? HandlesUtils.TransparentRed : HandlesUtils.TransparentGreen;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadious);
        }
#endif
        public virtual void TakeDamage()
        {
            _isJumping = false;

            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, DamageVelocity);
            Animator.SetTrigger(Hit);
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            Sounds.PlayClip("Melee");
        }

        public void OnAttack()
        {
            _attackRange.Check();
        }
    }
}
