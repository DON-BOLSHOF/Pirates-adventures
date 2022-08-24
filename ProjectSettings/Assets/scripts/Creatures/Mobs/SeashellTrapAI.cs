using Assets.scripts.Components;
using Assets.scripts.Components.ColliderBased;
using Assets.scripts.Model;
using System;
using UnityEngine;

namespace Assets.scripts.Creatures.Mobs
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
