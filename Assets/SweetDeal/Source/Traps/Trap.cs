using System;
using UnityEngine;

namespace SweetDeal.Source.Traps
{
    public abstract class Trap : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Activate();
        }

        protected abstract void Activate();
    }
}