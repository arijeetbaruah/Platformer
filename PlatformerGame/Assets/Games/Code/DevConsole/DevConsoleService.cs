using BP.Service;
using UnityEngine;

namespace BP.Console
{
    public class DevConsoleService : IService
    {
        private DevConsole console;
        
        public DevConsoleService(DevConsole console)
        {
            this.console = console;
            console.gameObject.SetActive(false);
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                console.gameObject.SetActive(!console.gameObject.activeSelf);
            }
        }

        public void OnDestroy()
        {
            
        }
    }
}
