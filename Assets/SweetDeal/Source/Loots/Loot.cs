using System;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Loot : Interaction
    {
        [SerializeField] private int amount;
        public int Amount => amount;
        public static event Action<Loot> OnLootCollected;

        protected override void InteractWith()
        {
            OnLootCollected?.Invoke(this);
        }
    }
}