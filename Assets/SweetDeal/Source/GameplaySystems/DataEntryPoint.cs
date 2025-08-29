using SweetDeal.Source.Gadgets.Inventory;
using SweetDeal.Source.Loots;
using SweetDeal.Source.Stealth;
using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public class DataEntryPoint : MonoBehaviour
    {
        [SerializeField] private BagScriptableObject _bag;
        
        public void Load()
        {
            var cargo = FindAnyObjectByType<Cargo>();
            var stepNoise = FindAnyObjectByType<StepNoise>();
            var toolsBar = FindAnyObjectByType<ToolsBar>();
            var factory = FindAnyObjectByType<GadgetFactory>();

            var improveData = DataKeeper.Load<HeroImproveData>(STRING_KEYS_CONSTRAINTS.HeroImproveKey);
            var equipment = DataKeeper.Load<EquipmentData>(STRING_KEYS_CONSTRAINTS.EquipmentKey);

            foreach (var eq in equipment.EquipmentNameAmountData)
            {
                Debug.Log($"{eq.Name} : {eq.Amount}");
            }

            if (improveData != null)
            {
                for (int i = 0; i < improveData.BagCount; i++)
                {
                    cargo.AddBag(_bag);   
                }
                stepNoise.SetNoiseModifier(improveData.BootsModifier);
            }

            if (equipment != null)
            {
                foreach (var rEquipmentNameAmountData in equipment.EquipmentNameAmountData)
                {
                    var gadget = factory.Create(rEquipmentNameAmountData.Name, rEquipmentNameAmountData.Amount);
                    toolsBar.Fill(gadget);
                }
            }
        }
    }
}