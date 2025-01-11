using System;
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
        
        [SerializeField] private Animator animator;
        
        private Rigidbody2D rigidBody2D;
        private PlayerController playerController;

        private void Awake()
        {
            TryGetComponent(out rigidBody2D);
            TryGetComponent(out playerController);
        }

        private void Update()
        {
            if (playerController.IsGrounded)
            {
                if (rigidBody2D.linearVelocityX == 0)
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
    }
}
