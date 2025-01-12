using System;
using System.Collections.Generic;
using UnityEngine;

namespace PG.Service
{
    public class ServiceManager : MonoBehaviour
    {
        public static ServiceManager Instance;

        private Dictionary<Type, IService> services = new();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(this);
                services = new();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public static T Get<T>() where T : class, IService
        {
            if (Instance != null && Instance.services.TryGetValue(typeof(T), out IService service))
            {
                return (T)service;
            }

            return null;
        }

        public static void Add<T>(T service) where T : class, IService
        {
            if (Instance == null)
            {
                Debug.Log("ServiceManager: Instance is null");
                
                return;
            }
            
            Instance.services.Add(typeof(T), service);
        }

        public static void Remove<T>() where T : class, IService
        {
            if (Instance == null)
            {
                Debug.Log("ServiceManager: Instance is null");
                
                return;
            }

            Instance.services.Remove(typeof(T));
        }
    }
}
