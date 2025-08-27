using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem.Configurable
{
    [CreateAssetMenu(fileName = "ShopGadgetCard", menuName = "SweetDeal/ShopGadgetCardDefinition")]
    public class ShopGadgetCard : ShopCardDefinition
    {
        [field: SerializeField] public GadgetDefinition GadgetDefinition { get; private set; }
    }
}