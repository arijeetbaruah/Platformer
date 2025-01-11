using System;
using UnityEngine;

namespace PG
{
    public class Hitbox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Hitbox entered: {other.gameObject.name}");
        }
    }
}
