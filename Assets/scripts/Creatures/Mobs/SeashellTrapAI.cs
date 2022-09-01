using PixelCrew.Components;
using System;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    class SeashellTrapAI : ShootingTrapAI
    {
        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private ColliderCheck _meleeCanAttack;

        private readonly static int Melee = Animator.StringToHash("melee");

        protected new void Update()
        {
            if (_meleeCanAttack.IsTouchingLayer && _meleeCooldown.IsReady)
            {
                MeleeAttack();
                return;
            }

            base.Update();
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
    }
}
