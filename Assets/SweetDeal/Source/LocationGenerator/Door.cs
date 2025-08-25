using System;
using SweetDeal.Source.Loots;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.LocationGenerator
{
    public class Door : Interaction
    {
        [SerializeField] private bool _canOpen;

        public UnityEvent onOpen;
        
        public bool CanOpen => _canOpen;
        
        public Vector3 Position => transform.position;

        public void Activate()
        {
            _canOpen = true;
        }

        protected override void InteractWith()
        {
            if (_canOpen)
                onOpen?.Invoke();
        }
    }
}