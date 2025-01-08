using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using PG.Service;
using UnityEngine;

namespace PG.Audio
{
    public class AudioService : IService
    {
        private List<EventInstance> eventInstances = new List<EventInstance>();

        ~AudioService()
        {
            foreach (var eventInstance in eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
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

        public EventInstance CreateInstance(EventReference audioReference)
        {
            EventInstance instance = RuntimeManager.CreateInstance(audioReference);
            eventInstances.Add(instance);
            
            return instance;
        }
    }
}
