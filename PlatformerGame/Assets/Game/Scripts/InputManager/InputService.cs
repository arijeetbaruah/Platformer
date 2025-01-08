using PG.Service;
using UnityEngine;

namespace PG.Input
{
    public class InputService : IService
    {
        private InputSystem _inputSystem;

        public InputSystem.PlayerActions Player => _inputSystem.Player;

        public InputService()
        {
            _inputSystem = new InputSystem();
            
        }
        
        public void Update()
        {
            
        }
    }
}
