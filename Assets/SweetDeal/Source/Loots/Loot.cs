using System;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private int _amount;
        
        public static event Action<Loot> OnLootCollected;
        
        public event Action OnLootEntered;
        public event Action OnLootExited;
        
        private bool _activated;

        public int Collect()
        {
            GetComponent<Collider>().enabled = false;
            OnLootExited?.Invoke();
            return _amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnLootCollected?.Invoke(this);
            OnLootEntered?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnLootExited?.Invoke();
        }
    }
}