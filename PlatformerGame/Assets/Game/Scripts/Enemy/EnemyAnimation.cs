using UnityEngine;

namespace PG.Enemy
{
    public abstract class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        
        private Targetable targetable;

        public void PlayAnimation(int animation)
        {
            animator.Play(animation);
        }
    }
}
