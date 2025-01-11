using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using PG.Audio;
using PG.Loading;
using PG.Service;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace PG.UI.MainMenu
{
    [System.Serializable]
    public class SceneAssetReference : AssetReference
    {
        #if UNITY_EDITOR
        public override bool ValidateAsset(Object obj)
        {
            return obj.GetType() == typeof(SceneAsset);
        }

        public override bool ValidateAsset(string path)
        {
            SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            return scene != null;
        }
#endif
    }
    
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private EventReference _backgroundMusic;
        
        [SerializeField] private SceneAssetReference additivieAsset;
        [SerializeField] private SceneAssetReference gameAsset;
        
        private IEnumerator Start()
        {
            yield return Addressables.LoadSceneAsync(additivieAsset, LoadSceneMode.Additive);
            
            ServiceManager.Add(new AudioService());
            
            ServiceManager.Get<AudioService>().PlayMusic(_backgroundMusic);
        }

        public void NewGame()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return ServiceManager.Get<LoadingService>().FadeIn();
            
            yield return Addressables.LoadSceneAsync(gameAsset, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
