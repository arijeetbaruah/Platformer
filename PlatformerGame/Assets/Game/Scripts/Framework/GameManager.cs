using System;
using FMOD.Studio;
using FMODUnity;
using PG.Audio;
using PG.Service;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace PG.Framework
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EventReference ambianceEvent;
        
        private EventInstance ambianceEventInstance;
        
        public Ambience CurrentAmbience { get; private set; }

        private void Start()
        {
            ServiceManager.Add(new GameService(this));
            
            ambianceEventInstance = ServiceManager.Get<AudioService>().CreateInstance(ambianceEvent);
            ambianceEventInstance.start();
        }

        private void OnDestroy()
        {
            ambianceEventInstance.stop(STOP_MODE.IMMEDIATE);
            ambianceEventInstance.release();
        }

        public void SetAmbience(Ambience ambience)
        {
            CurrentAmbience = ambience;
            ambianceEventInstance.setParameterByName("Ambience", (int)ambience);
        }
    }
}
