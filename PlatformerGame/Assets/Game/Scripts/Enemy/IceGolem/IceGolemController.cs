using System;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using PG.Audio;
using PG.Player;
using PG.Service;
using UnityEngine;

namespace PG.Enemy
{
    public class IceGolemController : MonoBehaviour
    {
        [SerializeField] private IceGolemAnimtion iceGolemAnimtion;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Targetable targetabe;
        [SerializeField] private float movementSpeed;
        [SerializeField] private EventReference deathAudio;

        private Queue<Transform> waypointsQueue = new();
        private bool isMoving = false;
        
        private Transform currentWaypoint;
        private Tween movementTween;

        private void Start()
        {
            waypointsQueue = new Queue<Transform>(waypoints);

            Move();
        }

        private void OnEnable()
        {
            targetabe.OnHitEvent += OnHit;
            targetabe.OnDeathEvent += OnDeath;
        }

        private void OnDisable()
        {
            targetabe.OnHitEvent -= OnHit;
            targetabe.OnDeathEvent -= OnDeath;
        }

        private void OnDeath(Targetable obj)
        {
            movementTween?.Kill();
            iceGolemAnimtion.PlayDeathAnimation();
            ServiceManager.Get<AudioService>().PlayOnce(deathAudio);
        }

        private void OnHit(Targetable obj)
        {
            movementTween?.Kill();
            DOVirtual.DelayedCall(1, Move);
        }

        private void Move()
        {
            iceGolemAnimtion.PlayWalkAnimation();
            
            currentWaypoint = waypointsQueue.Dequeue();
            if (waypointsQueue.Count == 0)
            {
                waypointsQueue = new Queue<Transform>(waypoints);
            }
            
            transform.localScale = new Vector3(currentWaypoint.localScale.x * Mathf.Sign(currentWaypoint.position.x - transform.position.x), currentWaypoint.localScale.y, currentWaypoint.localScale.z);
            movementTween = transform.DOMove(currentWaypoint.position, movementSpeed);
            movementTween.onComplete += Move;
        }
    }
}
