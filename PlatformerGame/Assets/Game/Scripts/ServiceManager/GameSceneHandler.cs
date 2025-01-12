using System.Collections;
using PG.Input;
using PG.Loading;
using PG.Service;
using Unity.Cinemachine;
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
            
            var player = playerPrefab.InstantiateAsync(spawnPoint.position, spawnPoint.rotation);
            
            yield return player;
            
            FindFirstObjectByType<CinemachineCamera>().Follow = player.Result.transform;
            
            yield return ServiceManager.Get<LoadingService>().FadeOut();
        }
    }
}
