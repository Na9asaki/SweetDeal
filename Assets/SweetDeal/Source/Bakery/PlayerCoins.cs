using System;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class PlayerCoins : MonoBehaviour
    {
        [field: SerializeField] public int goal { get; private set; } = 100;
        public int Coins { get; private set; }
        
        public event Action<int> OnChange;

        public bool Spend(int cost)
        {
            if (Coins >= cost)
            {
                Coins -= cost;
                OnChange?.Invoke(Coins);
                return true;
            }
            return false;
        }

        public void Add(int amount)
        {
            Coins += amount;
            OnChange?.Invoke(Coins);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(STRING_KEYS_CONSTRAINTS.PlayerCoinsKey, Coins);
            PlayerPrefs.Save();
        }
    }
}