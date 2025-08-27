using System;
using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    [Serializable]
    public abstract class Gadget
    {
        [field: SerializeField] public int UseNumbers { get; protected set; }

        public event Action OnEmpty;
        public string Name;
        
        public bool IsEmpty => UseNumbers == 0;
        public GadgetDefinition Definition { get; protected set; }

        public Gadget(int useNumbers, GadgetDefinition definition)
        {
            UseNumbers = useNumbers;
            Definition = definition;
            Name = definition.Name;
        }
        
        public abstract void Use();

        public void Empty()
        {
            OnEmpty?.Invoke();
        }
    }
}