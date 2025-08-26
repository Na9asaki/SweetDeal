using System;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class PuddleOil : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IStunned stunned))
            {
                stunned.Stun();
            }
        }
    }
}