using System;
using TMPro;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class PlayerCoinsView : MonoBehaviour
    {
        [SerializeField] private PlayerCoins playerCoins;
        [SerializeField] private TMP_Text coinText;

        private void OnEnable()
        {
            playerCoins.OnChange += UpdateInfo;
        }

        private void OnDisable()
        {
            playerCoins.OnChange -= UpdateInfo;
        }

        private void UpdateInfo(int coins)
        {
            coinText.text = $"For new Bakery\n{coins}/{playerCoins.goal}";
        }
    }
}