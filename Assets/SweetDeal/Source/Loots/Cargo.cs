using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Cargo : MonoBehaviour
    {
        private List<Bag> _bags = new List<Bag>();

        public event Action OnChange;
        public event Action<int> OnAdded;
        public IEnumerable<Bag> Bags => _bags;
        
        public int Capacity 
        {
            get
            {
                int value = 0;
                foreach (var bag in _bags)
                {
                    value += bag.Capacity;
                }

                return value;
            }
        }

        public int Count
        {
            get
            {
                int temp = 0;
                foreach (var bag in _bags)
                {
                    temp += bag.Count;
                }
                Debug.Log(temp);
                return temp;
            }
        }

        public void AddBag(BagScriptableObject bag)
        {
            _bags.Add(new Bag(bag));
            
            OnChange?.Invoke();
        }

        public void Fill(int amount)
        {
            int addedValue = 0;
            foreach (var bag in _bags)
            {
                if (bag.FreeCapacity < amount)
                {
                    addedValue += bag.FreeCapacity;
                    bag.AddCookie(bag.FreeCapacity);
                    amount -= bag.FreeCapacity;
                }
                else
                {
                    addedValue += amount;
                    bag.AddCookie(amount);
                    break;
                }
            }
            OnAdded?.Invoke(addedValue);
            OnChange?.Invoke();
        }

        public bool Spend(int amount)
        {
            int have = 0;
            foreach (var bag in _bags)
            {
                have += bag.Count;
            }

            if (have < amount)
            {
                return false;
            }
            
            foreach (var bag in _bags)
            {
                if (bag.Count >= amount)
                {
                    bag.RemoveCookie(amount);
                    break;
                }
                else
                {
                    bag.RemoveCookie(bag.Count);
                    amount -= bag.Count;
                }
            }
            OnChange?.Invoke();

            return true;
        }
    }
}