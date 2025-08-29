using System;
using SweetDeal.Source.Bakery.ShopSystem.Configurable;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;
using UnityEngine.UI;

namespace SweetDeal.Source.Bakery.ShopSystem
{
    public class ShopCardButton : MonoBehaviour
    {
        [SerializeField] private ShopCardDefinition shopCardDefinition;
        [SerializeField] private int itemAmount = 1;
        
        private Button _button;
        
        public static event Action<ShopCardDefinition, int> OnClick;

        private void OnEnable()
        {
            Shop.OnEnoughCoins += Activate;
            Shop.OnNoEnoughCoins += Deactivate;
            Shop.OnSalesStarted += Init;
        }

        private void OnDisable()
        {
            Shop.OnEnoughCoins -= Activate;
            Shop.OnNoEnoughCoins -= Deactivate;
            Shop.OnSalesStarted -= Init;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(Change);
        }

        private void Init(int coins)
        {
            if (coins < shopCardDefinition.Price)
            {
                Deactivate();
            }
        }

        private void Activate()
        {
            _button.interactable = true;
        }

        private void Deactivate()
        {
            if (itemAmount < 0)
            {
                return;
            }
            _button.interactable = false;
        }

        private void Change()
        { 
            OnClick?.Invoke(shopCardDefinition, itemAmount);
        }
    }
}