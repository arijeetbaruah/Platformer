using System.Collections.Generic;
using System.Linq;
using PG.Service;

namespace PG.Framework.Interaction
{
    public class InteractionService : IService
    {
        private InteractionCanvas minteractionCanvas;
        
        private HashSet<IInteractable> interactables = new ();
        
        public IEnumerable<IInteractable> Interactables => interactables.ToList();

        public InteractionService(InteractionCanvas interactionCanvas)
        {
            minteractionCanvas = interactionCanvas;
        }
        
        public void Update()
        {
            
        }

        public void AddInteractable(IInteractable interactable)
        {
            interactables.Add(interactable);

            if (interactables.Count > 0)
            {
                minteractionCanvas.OpenPanel();
            }
        }

        public void RemoveInteractable(IInteractable interactable)
        {
            interactables.Remove(interactable);
            
            if (interactables.Count == 0)
            {
                minteractionCanvas.ClosePanel();
            }
        }
    }
}
