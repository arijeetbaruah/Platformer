using System;
using System.Collections;
using BP.Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BP.Input
{
    public class InputEvents : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return null;
            
            GetComponent<InputEvents>().enabled = true;
        }

        public void OnDeviceChanged(PlayerInput playerInput)
        {
            ServiceManager.Get<InputService>().OnDeviceChanged();
        }
    }
}
