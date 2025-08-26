using UnityEngine;

namespace SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable
{
    [CreateAssetMenu(fileName = "Definition", menuName = "SweetDeal/GadgetDefinition", order = 0)]
    public class GadgetDefinition : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}