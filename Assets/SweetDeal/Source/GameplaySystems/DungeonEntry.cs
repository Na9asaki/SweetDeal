using System.Collections.Generic;
using SweetDeal.Source.Gadgets.Inventory;
using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Loots;
using SweetDeal.Source.Scenes;
using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public class DungeonEntry : MonoBehaviour
    {
        private Door _door;
        private void Awake()
        {
            _door = GetComponent<Door>();
        }

        private void OnEnable()
        {
            _door.onInteractionUsedUnityEvent.AddListener(Exit);
        }

        private void OnDisable()
        {
            _door.onInteractionUsedUnityEvent.RemoveListener(Exit);
        }

        private void Exit()
        {
            var cargo = FindAnyObjectByType<Cargo>();
            var toolsBar = FindAnyObjectByType<ToolsBar>();
            int amount = cargo.Count;
            CookieEquipmentData data = new()
            {
                cookies = amount
            };
            EquipmentData data2 = new();

            foreach (var gadget in toolsBar.Gadgets)
            {
                if (gadget != null)
                    data2.EquipmentNameAmountData.Add(new EquipmentNameAmountData()
                    {
                        Amount = gadget.UseNumbers,
                        Name = gadget.Name
                    });
            }
            DataKeeper.Save(data2, STRING_KEYS_CONSTRAINTS.EquipmentKey);
            DataKeeper.Save(data, STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);
            
            LevelLoader.LoadMenu();
        }
    }
}