using System;
using System.Linq;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class PlayerContainer : MonoBehaviour
    {
        public EquipmentData equipmentData;
        public HeroImproveData heroImproveData;
        public PlayerCoins playerCoins;

        public int Bags => heroImproveData.BagCount;
        public float BootsBoost => heroImproveData.BootsModifier;

        EquipmentNameAmountData Find(EquipmentNameAmountData equipmentData)
        {
            return this.equipmentData.EquipmentNameAmountData
                .FirstOrDefault(x => x.Name.Equals(equipmentData.Name));
        }

        public void AddEquipment(EquipmentData equipmentData)
        {
            foreach (var equipmentNameAmountData in equipmentData.EquipmentNameAmountData)
            {
                var existingEquip = Find(equipmentNameAmountData);
                if (existingEquip != null)
                {
                    existingEquip.Amount += equipmentNameAmountData.Amount;
                }
                else
                {
                    this.equipmentData.EquipmentNameAmountData.Add(equipmentNameAmountData);
                }
            }
        }

        public void AddImprove(HeroImproveData improveData)
        {
            this.heroImproveData.BootsModifier += improveData.BootsModifier;
            this.heroImproveData.BagCount += improveData.BagCount;
        }

        public void Init()
        {
            heroImproveData = DataKeeper.Load<HeroImproveData>(STRING_KEYS_CONSTRAINTS.HeroImproveKey);
            equipmentData = DataKeeper.Load<EquipmentData>(STRING_KEYS_CONSTRAINTS.EquipmentKey);
            var value = PlayerPrefs.GetInt(STRING_KEYS_CONSTRAINTS.PlayerCoinsKey, 0);
            playerCoins.Add(value);
            if (heroImproveData == null)
            {
                heroImproveData = new HeroImproveData();
                heroImproveData.BagCount = 1;
            }

            if (equipmentData == null)
            {
                equipmentData = new EquipmentData();
            }
        }

        public void Save()
        {
            DataKeeper.Save(heroImproveData, STRING_KEYS_CONSTRAINTS.HeroImproveKey);
            DataKeeper.Save(equipmentData, STRING_KEYS_CONSTRAINTS.EquipmentKey);
        }

        private void OnDestroy()
        {
            Save();
        }
    }
}