using System;
using DG.Tweening;
using PG.Player;
using UnityEngine;

namespace PG
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public readonly int IdleAnimation = Animator.StringToHash("Idle");
        public readonly int WalkAnimation = Animator.StringToHash("Walk");
        public readonly int JumpAnimation = Animator.StringToHash("Jump");
        public readonly int FallAnimation = Animator.StringToHash("Fall");
        public readonly int HitAnimation = Animator.StringToHash("Hit");
        public readonly int Attack1Animation = Animator.StringToHash("Attack1");
        public readonly int DeathAnimation = Animator.StringToHash("Die");
        
        [SerializeField] private Animator animator;
        
        private Rigidbody2D rigidBody2D;
        private PlayerController playerController;
        private Targetable targetable;

        private bool isHit = false;
        
        private void Awake()
        {
            TryGetComponent(out rigidBody2D);
            TryGetComponent(out playerController);
            TryGetComponent(out targetable);
        }

        private void OnEnable()
        {
            targetable.OnHitEvent += OnHit;
            targetable.OnDeathEvent += OnDeath;
        }

        private void OnDisable()
        {
            targetable.OnHitEvent -= OnHit;
            targetable.OnDeathEvent -= OnDeath;
        }

        private void Update()
        {
            if (playerController.IsAttacking || isHit || targetable.IsDeath) return;
            
            if (playerController.IsGrounded)
            {
                if (playerController.Movement.x == 0)
                {
                    animator.Play(IdleAnimation);
                }
                else
                {
                    animator.Play(WalkAnimation);
                }
            }
            else
            {
                if (rigidBody2D.linearVelocityY > 0)
                {
                    animator.Play(JumpAnimation);
                }
                else if (rigidBody2D.linearVelocityY < 0)
                {
                    animator.Play(FallAnimation);
                }
            }
        }

        public void Attack(Action callback = null)
        {
            if (targetable.IsDeath) return;

            animator.Play(Attack1Animation);
            DOVirtual.DelayedCall(animator.GetCurrentAnimatorClipInfo(0).Length, () => callback?.Invoke());
        }

        private void OnHit(Targetable targetable)
        {
            if (targetable.IsDeath) return;
            
            isHit = true;
            animator.Play(HitAnimation);
            DOVirtual.DelayedCall(animator.GetCurrentAnimatorClipInfo(0).Length, () => isHit = false);
        }

        private void OnDeath(Targetable targetable)
        {
            animator.Play(DeathAnimation);
        }
    }
}
