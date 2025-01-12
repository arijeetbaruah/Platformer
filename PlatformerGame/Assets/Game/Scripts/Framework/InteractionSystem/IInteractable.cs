using System;
using PG.Service;
using UnityEngine;

namespace PG.Framework.Interaction
{
    public interface IInteractable
    {
        void Interact();
    }

    public abstract class BaseInteractable : MonoBehaviour, IInteractable
    {
        public abstract void Interact();

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            ServiceManager.Get<InteractionService>().AddInteractable(this);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            ServiceManager.Get<InteractionService>()?.RemoveInteractable(this);
        }
    }
}
