using System;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Loot : Interaction
    {
        [SerializeField] private int amount;
        public int Amount => amount;
        public static event Action<Loot> OnLootCollected;

        private bool _collected = false;

        protected override void InteractWith()
        {
            if (_collected) return;
            _collected = true;
            OnLootCollected?.Invoke(this);
        }
    }
}