using PG.Service;
using UnityEngine;

namespace PG.Framework.Interaction
{
    public class Chest : BaseInteractable
    {
        private readonly int ClosedAnimation = Animator.StringToHash("Closed");
        private readonly int OpeningAnimation = Animator.StringToHash("Opening");
        private readonly int OpenedAnimation = Animator.StringToHash("Opened");
        
        [SerializeField] private bool isOpened;
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator.Play(isOpened ? OpenedAnimation : ClosedAnimation);
        }

        public override void Interact()
        {
            Debug.Log("Interacting with Chest");
            isOpened = true;
            animator.Play(OpeningAnimation);
            
            ServiceManager.Get<InteractionService>().RemoveInteractable(this);
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (isOpened) return;
            
            base.OnTriggerEnter2D(other);
        }
    }
}
