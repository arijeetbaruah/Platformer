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

        private EventInstance _musicInstance;
        
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

        public void PlayMusic(EventReference audioReference)
        {
            _musicInstance = CreateInstance(audioReference);
            _musicInstance.start();
        }

        public void StopMusic()
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void StopAndReleaseMusic()
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _musicInstance.release();
            eventInstances.Remove(_musicInstance);
        }

        public EventInstance CreateInstance(EventReference audioReference)
        {
            EventInstance instance = RuntimeManager.CreateInstance(audioReference);
            eventInstances.Add(instance);
            
            return instance;
        }
    }
}
