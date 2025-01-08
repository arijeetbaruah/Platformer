using PG.Audio;
using PG.Service;
using UnityEngine;

namespace PG.UI.MainMenu
{
    public class MainMenuPanel : MonoBehaviour
    {
        private void Start()
        {
            ServiceManager.Add(new AudioService());
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
