using PG.Input;
using PG.Service;
using UnityEngine;

namespace PG
{
    public class GameSceneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;
        
        private void Start()
        {
            ServiceManager.Add(new InputService());
            
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
