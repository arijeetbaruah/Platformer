using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PG
{
    public class Targetable : MonoBehaviour
    {
        [SerializeField] private int maxHitPoints = 1;
        
        [ShowInInspector] public int currentHitPoints { get; private set; }
        
        private bool _isDeath = false;

        public Action<Targetable> OnHitEvent;
        public Action<Targetable> OnDeathEvent;
        
        public bool IsDeath => _isDeath;

        private void Start()
        {
            currentHitPoints = maxHitPoints;
        }

        public void OnHit(int hp)
        {
            currentHitPoints = Mathf.Max(currentHitPoints - hp, 0);
            if (currentHitPoints == 0)
            {
                _isDeath = true;
                OnDeathEvent?.Invoke(this);
            }
            else
            {
                OnHitEvent?.Invoke(this);
            }
        }
    }
}
