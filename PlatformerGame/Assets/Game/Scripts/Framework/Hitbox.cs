using System;
using System.Collections.Generic;
using UnityEngine;

namespace PG
{
    public class Hitbox : MonoBehaviour
    {
        private List<GameObject> hits = new();
        [SerializeField] private GameObject owner;

        public Action<Targetable> OnAttackHit = delegate { };
        
        private void OnEnable()
        {
            hits.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Targetable target = other.GetComponent<Targetable>();
           if (hits.Contains(other.gameObject) || other.gameObject == owner || target == null)
               return;
           
           hits.Add(other.gameObject);
           OnAttackHit.Invoke(target);
        }
    }
}
