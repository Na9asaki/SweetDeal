using System;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class PuddleOil : MonoBehaviour
    {
        [SerializeField] private float timeStun = 2f;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStunned stunned))
            {
                stunned.Stun(timeStun);
            }
        }
    }
}