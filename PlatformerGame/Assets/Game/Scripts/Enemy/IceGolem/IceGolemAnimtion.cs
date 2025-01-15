using DG.Tweening;
using UnityEngine;

namespace PG.Enemy
{
    public class IceGolemAnimtion : EnemyAnimation
    {
        private readonly int idleAnimation = Animator.StringToHash("Idle");
        private readonly int walkAnimation = Animator.StringToHash("Walk");
        private readonly int deathAnimation = Animator.StringToHash("Death");

        public void PlayIdleAnimation()
        {
            PlayAnimation(idleAnimation);
        }

        public void PlayWalkAnimation()
        {
            PlayAnimation(walkAnimation);
        }

        public void PlayDeathAnimation()
        {
            PlayAnimation(deathAnimation);
            var length = animator.GetCurrentAnimatorClipInfo(0).Length;
            DOVirtual.DelayedCall(length, () => Destroy(gameObject));
        }
    }
}
