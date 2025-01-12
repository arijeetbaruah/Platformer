using System;
using System.Collections;
using System.Collections.Generic;
using PG.Input;
using PG.Service;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PG.Framework.Interaction
{
    public class InteractionCanvas : MonoBehaviour
    {
        [SerializeField] private Transform panel;
        [SerializeField] private TextMeshProUGUI interactionText;
        
        private IEnumerator Start()
        {
            ServiceManager.Add(new InteractionService(this));
            yield return new WaitUntil(() => ServiceManager.Get<InputService>() == null);

            string inputString = ServiceManager.Get<InputService>().Player.Interact.GetBindingDisplayString(group: "Keyboard&Mouse");
            
            interactionText.SetText($"{inputString} to interact");
        }

        private void OnDestroy()
        {
            if (ServiceManager.Instance != null)
                ServiceManager.Remove<InteractionService>();
        }

        public void OpenPanel()
        {
            panel.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            panel.gameObject.SetActive(false);
        }
    }
}
