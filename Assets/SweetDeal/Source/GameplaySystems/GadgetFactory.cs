using SweetDeal.Source.Gadgets;
using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public class GadgetFactory : MonoBehaviour
    {
        [SerializeField] private GadgetDefinition grenade;
        [SerializeField] private GadgetDefinition oil;
        [SerializeField] private Transform player;
        [SerializeField] private Transform spawnPoint;
        
        public Gadget Create(string name, int useNumbers)
        {
            switch (name)
            {
                case "Grenade":
                    return new FuneralGrenade(spawnPoint, player, useNumbers, grenade);
                case "Oil":
                    return new Oil(spawnPoint, player, useNumbers, oil);
            }

            return null;
        }
    }
}