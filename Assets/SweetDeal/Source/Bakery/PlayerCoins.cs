using System;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class PlayerCoins : MonoBehaviour
    {
        public int Coins { get; private set; }

        public bool Spend(int cost)
        {
            if (Coins >= cost)
            {
                Coins -= cost;
                return true;
            }
            return false;
        }

        public void Add(int amount)
        {
            Coins += amount;
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(STRING_KEYS_CONSTRAINTS.PlayerCoinsKey, Coins);
            PlayerPrefs.Save();
        }
    }
}