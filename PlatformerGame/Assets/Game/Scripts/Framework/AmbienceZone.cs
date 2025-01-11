using System;
using PG.Service;
using UnityEngine;

namespace PG.Framework
{
    public enum Ambience
    {
        Forest,
        Castle
    }
    
    public class AmbienceZone : MonoBehaviour
    {
        [SerializeField] private Ambience ambience = Ambience.Forest;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log($"Ambience Zone: {ambience}");
            ServiceManager.Get<GameService>().Manager.SetAmbience(ambience);
        }
    }
}
