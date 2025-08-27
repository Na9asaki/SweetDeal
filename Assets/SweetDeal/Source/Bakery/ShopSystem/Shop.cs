using System;
using System.Collections.Generic;
using SweetDeal.Source.Bakery.ShopSystem.Configurable;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private PlayerCoins playerCoins;
        [SerializeField] private PlayerContainer playerContainer;
        
        public static event Action<int> OnSalesStarted;
        public static event Action OnNoEnoughCoins; 
        public static event Action OnEnoughCoins;
        public static event Action<Dictionary<ShopCardDefinition, int>> OnBought;
        public static event Action<ShopCardDefinition, int> OnConfirm;
        public static event Action<int, int> OnCostChange;

        private Dictionary<ShopCardDefinition, int> _basket =  new Dictionary<ShopCardDefinition, int>();

        private bool _coinsState;

        public int FullCost {get; private set;}

        public void Activate()
        {
            OnSalesStarted?.Invoke(playerCoins.Coins);
            OnCostChange?.Invoke(FullCost, playerCoins.Coins);
        }

        private void OnEnable()
        {
            ShopCardButton.OnClick += HandleButtonClick;
        }

        private void OnDisable()
        {
            ShopCardButton.OnClick -= HandleButtonClick;
        }

        private void HandleButtonClick(ShopCardDefinition shopCardDefinition, int amount)
        {
            if (amount < 0) Remove(shopCardDefinition);
            else Add(shopCardDefinition);
        }

        private void Add(ShopCardDefinition shopCardDefinition)
        {
            if (shopCardDefinition is ShopImproveCard improveCard)
            {
                switch (improveCard.ImproveCategories)
                {
                    case ImproveCategories.Boots:
                        var value = _basket.TryGetValue(shopCardDefinition, out int amount);
                        if (Mathf.Approximately(playerContainer.BootsBoost + 0.2f * amount, improveCard.MaxAmount))
                        {
                            return;
                        }
                        break;
                    case ImproveCategories.Inventory:
                        var value2 = _basket.TryGetValue(shopCardDefinition, out int amount2);
                        if (playerContainer.Bags + amount2 == (int)improveCard.MaxAmount)
                        {
                            return;
                        }
                        break;
                }
            }
            
            
            FullCost += shopCardDefinition.Price;
            if (playerCoins.Coins < FullCost)
            {
                FullCost -= shopCardDefinition.Price;
                OnNoEnoughCoins?.Invoke();
                return;
            }

            if (!_basket.TryAdd(shopCardDefinition, 1))
            {
                _basket[shopCardDefinition] += 1;   
            }
            OnConfirm?.Invoke(shopCardDefinition, _basket[shopCardDefinition]);
            OnCostChange?.Invoke(FullCost, playerCoins.Coins);
        }

        private void Remove(ShopCardDefinition shopCardDefinition)
        {
            if (!_basket.TryGetValue(shopCardDefinition, out var value)) return;
            if (value == 0) return;
            FullCost -= shopCardDefinition.Price;
            _basket[shopCardDefinition] -= 1;
            if (playerCoins.Coins > FullCost)
            {
                OnEnoughCoins?.Invoke();
            }
            OnConfirm?.Invoke(shopCardDefinition, _basket[shopCardDefinition]);
            
            OnCostChange?.Invoke(FullCost, playerCoins.Coins);
        }
        
        public void Buy()
        {
            if (playerCoins.Coins >= FullCost)
            {
                playerCoins.Spend(FullCost);
                FullCost = 0;
                OnBought?.Invoke(_basket);
            }
        }
    }
}