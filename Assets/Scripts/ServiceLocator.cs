using UnityEngine;
using System.Collections.Generic;
using System;

namespace ServiceLocatorPattern
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("ServiceLocator", typeof(ServiceLocator)).GetComponent<ServiceLocator>();
                }

                return _instance;
            }

            private set 
            {
                _instance = value;
            }
        }

        private Dictionary<Type, GameObject> services = new Dictionary<Type, GameObject>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Register(Type type, GameObject serviceInstance)
        {
            if (!services.ContainsKey(type))
            {
                services.Add(type, serviceInstance);
            }
            else
            {
                services[type] = serviceInstance;
            }
        }

        public void Unregister(Type type)
        {
            if (services.ContainsKey(type))
            {
                services.Remove(type);
            }
        }

        public GameObject GetService(Type type)
        {
            if (services.TryGetValue(type, out GameObject service))
            {
                return service;
            }

            Debug.LogError("Service not registered");
            return null;
        }
    }
}

