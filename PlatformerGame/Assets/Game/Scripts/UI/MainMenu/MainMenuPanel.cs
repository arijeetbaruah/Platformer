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
using UnityEngine.Serialization;

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
        [SerializeField] private List<AssetReference> mainMenuBanks;
        
        [SerializeField] private List<AssetReference> gameBanks;
        
        private IEnumerator Start()
        {
            foreach (var bank in mainMenuBanks)
            {
                RuntimeManager.LoadBank(bank, true);
            }
            
            // Keep yielding the co-routine until all the bank loading is done
            // (for platforms with asynchronous bank loading)
            yield return new WaitWhile(() => !RuntimeManager.HaveAllBanksLoaded || RuntimeManager.AnySampleDataLoading());
            
            Debug.Log("Loaded Fmod");
            
            yield return Addressables.LoadSceneAsync(additivieAsset, LoadSceneMode.Additive);

            var audioService = new AudioService();
            ServiceManager.Add(audioService);
            
            audioService.PlayMusic(_backgroundMusic);
        }

        public void NewGame()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return ServiceManager.Get<LoadingService>().FadeIn();
            
            foreach (var bank in gameBanks)
            {
                RuntimeManager.LoadBank(bank, true);
            }
            
            // Keep yielding the co-routine until all the bank loading is done
            // (for platforms with asynchronous bank loading)
            yield return new WaitWhile(() => !RuntimeManager.HaveAllBanksLoaded || RuntimeManager.AnySampleDataLoading());
            
            var loader = Addressables.LoadSceneAsync(gameAsset, LoadSceneMode.Additive);

            while (!loader.IsDone)
            {
                ServiceManager.Get<LoadingService>().SetProgress(loader.PercentComplete);
                
                yield return null;
            }
            
            SceneManager.UnloadSceneAsync(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
