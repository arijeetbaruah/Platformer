using System.Collections;
using PG.Input;
using PG.Loading;
using PG.Service;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PG
{
    public class GameSceneHandler : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        private IEnumerator Start()
        {
            ServiceManager.Add(new InputService());
            
            yield return playerPrefab.InstantiateAsync(spawnPoint.position, spawnPoint.rotation);
            
            yield return ServiceManager.Get<LoadingService>().FadeOut();
        }
    }
}
