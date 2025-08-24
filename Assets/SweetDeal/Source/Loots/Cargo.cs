using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class Cargo : MonoBehaviour
    {
        [SerializeField] private BagScriptableObject _bag;
        
        private List<Bag> _bags = new List<Bag>();
        
        #if UNITY_EDITOR
        private void Awake()
        {
            AddBag(_bag);
        }
#endif

        public void AddBag(BagScriptableObject bag)
        {
            _bags.Add(new Bag(bag));
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
        }
    }
}