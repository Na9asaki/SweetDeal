using System;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.Loots
{
    public abstract class Interaction : MonoBehaviour
    {
        [SerializeField] private bool _colliderEnabled = false;
        
        public UnityEvent onInteractionEnteredUnityEvent;
        public UnityEvent onInteractionExitedUnityEvent;
        public UnityEvent onInteractionUsedUnityEvent;
        
        public event Action OnLootEntered;
        public event Action OnLootExited;
        
        public static event Action<Interaction> OnInteractionEntered;
        
        private bool _activated;

        public void Interact()
        {
            GetComponent<Collider>().enabled = _colliderEnabled;
            OnLootExited?.Invoke();
            onInteractionUsedUnityEvent?.Invoke();
            InteractWith();
        }
        
        protected abstract void InteractWith();

        private void OnTriggerEnter(Collider other)
        {
            OnLootEntered?.Invoke();
            OnInteractionEntered?.Invoke(this);
            onInteractionEnteredUnityEvent?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnLootExited?.Invoke();
            onInteractionExitedUnityEvent?.Invoke();
        }
    }
}