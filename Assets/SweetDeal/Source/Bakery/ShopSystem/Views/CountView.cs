using System;
using SweetDeal.Source.Bakery.ShopSystem.Configurable;
using TMPro;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem.Views
{
    public class CountView : MonoBehaviour
    {
        [SerializeField] private ShopCardDefinition shopCardDefinition;
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void ConfirmChange(ShopCardDefinition shopCardDefinition, int amount)
        {
            if (this.shopCardDefinition == shopCardDefinition)
            {
                _text.text = amount.ToString();
            }
        }

        private void ResetTheText(int coins)
        {
            _text.text = "0";
        }

        private void OnEnable()
        {
            Shop.OnConfirm += ConfirmChange;
            Shop.OnSalesStarted += ResetTheText;
        }

        private void OnDisable()
        {
            Shop.OnConfirm -= ConfirmChange;
            Shop.OnSalesStarted -= ResetTheText;
        }
    }
}