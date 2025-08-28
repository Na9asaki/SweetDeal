using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem.Configurable
{
    [CreateAssetMenu(fileName = "ShopImproveCard", menuName = "SweetDeal/ShopImproveCardDefinition")]
    
    public class ShopImproveCard :  ShopCardDefinition
    {
        [field: SerializeField] public float Boost { get; private set; }
        [field: SerializeField] public ImproveCategories ImproveCategories { get; private set; }
    }
}