using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Cargo : MonoBehaviour
    {
        private int _bagCount;
        private int _bagCapacity;
        private int _counts;

        public event Action OnChange;
        public event Action<int> OnAdded;
        
        public int Capacity => _bagCount * _bagCapacity;

        public int Count => _counts;
        
        public int Free => Capacity - _counts;

        public void AddBag(BagScriptableObject bag)
        {
            _bagCount++;
            _bagCapacity = bag.Capacity;
            OnChange?.Invoke();
        }

        public void Fill(int amount)
        {
            int addedValue = 0;
            if (Free >= amount)
            {
                addedValue = amount;
                _counts += amount;
            }
            else
            {
                addedValue = Free;
                _counts += Free;
            }
            OnAdded?.Invoke(addedValue);
            OnChange?.Invoke();
        }

        public bool Spend(int amount)
        {
            if (_counts >= amount)
            {
                _counts -= amount;
                OnChange?.Invoke();
                return true;
            }

            return false;
        }
    }
}