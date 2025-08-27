using UnityEngine;

namespace SweetDeal.Source.Bakery.ShopSystem.Configurable
{
    public abstract class ShopCardDefinition : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public bool CanBeInfinity {get; private set;}
        [field: SerializeField] public float MaxAmount { get; private set; }
    }
}