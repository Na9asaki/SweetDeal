using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator.Configs
{
    [CreateAssetMenu(fileName = "LootDefinition", menuName = "SweetDeal/LootGeneratorConfig")]
    public class GeneratedLootDefinition : ScriptableObject
    {
        [field: SerializeField] public Loot[] GeneratedLoots { get; private set; }
        [field: SerializeField] public Loot[] Cookies { get; private set; }
    }
}