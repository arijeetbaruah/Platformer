using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using PG.Audio;
using PG.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PG.UI.MainMenu
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private EventReference _backgroundMusic;
        
        private void Start()
        {
            ServiceManager.Add(new AudioService());
            
            ServiceManager.Get<AudioService>().PlayMusic(_backgroundMusic);
        }

        public void NewGame()
        {
            StartCoroutine(LoadScene(1));
        }

        private IEnumerator LoadScene(int sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
