using System;
using BP.Service;
using UnityEngine;

namespace BP.Input
{
    public class InputService : IService
    {
        public InputControls controls;
        
        public Action OnDeviceChanged = delegate { };

        public InputService()
        {
            controls = new InputControls();
        }
        
        public void Update()
        {
        }

        public void OnDestroy()
        {
        }
    }
}
