using System;
using TMPro;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem.Views
{
    public class FullCostView : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void UpdateInfo(int cost, int coins)
        {
            _text.text = $"{coins} / {cost}";
        }

        private void OnEnable()
        {
            Shop.OnCostChange += UpdateInfo;
        }

        private void OnDisable()
        {
            Shop.OnCostChange -= UpdateInfo;
        }
    }
}