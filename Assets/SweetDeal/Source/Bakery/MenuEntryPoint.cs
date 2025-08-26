using System;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class MenuEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            var a = DataKeeper.Load<EquipmentData>(STRING_KEYS_CONSTRAINTS.EquipmentKey);
            var b = DataKeeper.Load<CookieEquipmentData>(STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);

            foreach (var pair in a.EquipmentNameAmountData)
            {
                Debug.Log(pair.Name + ": " + pair.Amount);
            }
            
            Debug.Log(b.cookies);
        }
    }
}