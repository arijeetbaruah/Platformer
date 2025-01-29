using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BP.Service
{
    public class ServiceManager : MonoBehaviour
    {
        private static ServiceManager _instance;
        
        [ShowInInspector]
        private Dictionary<Type, IService> services = new();

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static TService Get<TService>() where TService : class, IService
        {
            if (_instance.services.TryGetValue(typeof(TService), out var service))
            {
                return (TService)service;
            }

            return null;
        }

        public static void Add<TService>(TService service) where TService : class, IService
        {
            _instance.services.Add(typeof(TService), service);
        }

        public static void Remove<TService>() where TService : class, IService
        {
            _instance.services.Remove(typeof(TService));
        }

        private void Update()
        {
            foreach (var service in _instance.services.Values)
            {
                service.Update();
            }
        }
    }
}
