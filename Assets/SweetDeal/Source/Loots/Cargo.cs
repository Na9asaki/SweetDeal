using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Cargo : MonoBehaviour
    {
        private List<Bag> _bags = new List<Bag>();

        public event Action OnAdded;
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
            
            OnAdded?.Invoke();
        }

        public void Fill(int amount)
        {
            foreach (var bag in _bags)
            {
                if (bag.FreeCapacity <= amount)
                {
                    bag.AddCookie(bag.FreeCapacity);
                    amount -= bag.FreeCapacity;
                }
                else
                {
                    bag.AddCookie(amount);
                    break;
                }
            }
            OnAdded?.Invoke();
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
                    bag.AddCookie(-amount);
                    break;
                }
                else
                {
                    bag.AddCookie(-bag.Count);
                    amount -= bag.Count;
                }
            }
            OnAdded?.Invoke();

            return true;
        }
    }
}