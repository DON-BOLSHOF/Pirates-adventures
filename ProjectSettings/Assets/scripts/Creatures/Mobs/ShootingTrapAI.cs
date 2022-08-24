using Assets.scripts.Components;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Model;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs
{
    class ShootingTrapAI:MonoBehaviour
    {
        [SerializeField] public ColliderCheck _vision;

        [Header("Range")]
        [SerializeField] protected Cooldown _rangeCooldown;
        [SerializeField] protected SpawnComponent _rangeAttack;

        protected readonly static int Range = Animator.StringToHash("range");

        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        protected void Update()
        {
            if (_vision.IsTouchingLayer && _rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

        public void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

    }
}
