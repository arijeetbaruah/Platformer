using PG.Service;
using UnityEngine;

namespace PG.Framework
{
    public class GameService : IService
    {
        public GameManager Manager { get; private set; }

        public GameService(GameManager manager)
        {
            Manager = manager;
        }
        
        public void Update()
        {
            
        }
    }
}
