using System.Collections.Generic;
using System.Linq;
using SweetDeal.Source.Bakery.ShopSystem.Configurable;
using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem
{
    public class Supply : MonoBehaviour
    {
        [SerializeField] private PlayerContainer playerContainer;
        private void OnEnable()
        {
            Shop.OnBought += SaveBasket;
        }

        private void OnDisable()
        {
            Shop.OnBought -= SaveBasket;
        }

        private void SaveBasket(Dictionary<ShopCardDefinition, int> basket)
        {
            var equip = basket.Where(x => x.Key is ShopGadgetCard);
            
            var data = new EquipmentData();

            foreach (var card in equip)
            {
                if (card.Value == 0) continue;
                var gadgetName = ((ShopGadgetCard)card.Key).GadgetDefinition.Name;
                data.EquipmentNameAmountData.Add(new EquipmentNameAmountData()
                {
                    Amount = card.Value,
                    Name = gadgetName
                });
            }

            var data2 = new HeroImproveData();
            var improves = basket.Where(x => x.Key is ShopImproveCard);

            foreach (var improve in improves)
            {
                var castedImprove =  (ShopImproveCard)improve.Key;
                switch (castedImprove.ImproveCategories)
                {
                    case ImproveCategories.Boots:
                        data2.BootsModifier = castedImprove.Boost * improve.Value;
                        break;
                    case ImproveCategories.Inventory:
                        data2.BagCount = (int)castedImprove.Boost * improve.Value;
                        break;
                }
            }
            playerContainer.AddEquipment(data);
            playerContainer.AddImprove(data2);
            basket.Clear();
        }
    }
}