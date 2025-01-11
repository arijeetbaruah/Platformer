using System;
using System.Collections.Generic;
using UnityEngine;

namespace PG
{
    public class Hitbox : MonoBehaviour
    {
        private List<GameObject> hits = new();
        [SerializeField] private GameObject owner;

        private void OnEnable()
        {
            hits.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other)
       {
           if (hits.Contains(other.gameObject) || other.gameObject == owner)
               return;
           
           hits.Add(other.gameObject);
           Debug.Log($"Hitbox entered: {other.gameObject.name}");
        }
    }
}
