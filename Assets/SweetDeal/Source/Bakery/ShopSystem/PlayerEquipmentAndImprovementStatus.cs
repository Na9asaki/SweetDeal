using System;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem
{
    public class PlayerEquipmentAndImprovementStatus : MonoBehaviour
    {
        public static event Action<HeroImproveData> OnInit; 
        
        public void Init()
        {
            var data = DataKeeper.Load<HeroImproveData>(STRING_KEYS_CONSTRAINTS.HeroImproveKey);
            if (data != null)
            {
                OnInit?.Invoke(data);
            }
        }
    }
}