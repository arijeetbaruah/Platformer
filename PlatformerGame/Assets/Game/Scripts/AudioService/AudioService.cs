using FMODUnity;
using PG.Service;
using UnityEngine;

namespace PG.Audio
{
    public class AudioService : IService
    {
        public AudioService()
        {
            
        }
        
        public void Update()
        {
            
        }

        public void PlayOnce(EventReference audioReference)
        {
            RuntimeManager.PlayOneShot(audioReference);
        }
        
        public void PlayOnce(EventReference audioReference, Vector3 worldPosition)
        {
            RuntimeManager.PlayOneShot(audioReference, worldPosition);
        }
    }
}
