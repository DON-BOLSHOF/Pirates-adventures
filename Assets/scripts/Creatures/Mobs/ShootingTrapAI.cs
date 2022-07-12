using Assets.scripts.Components;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Model;
using System;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs
{
    class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision;

        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;

        [Header("Range")]
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;

        private Animator _animator;

        private readonly static int Melee = Animator.StringToHash("melee");
        private readonly static int Range = Animator.StringToHash("range"); 

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_meleeCanAttack.IsTouchingLayer && _meleeCooldown.IsReady)
                {
                    MeleeAttack();
                    return;
                }

                if (_rangeCooldown.IsReady)
                    RangeAttack();
            }

        }

        private void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            _animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }
        
        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

        
    }
}
