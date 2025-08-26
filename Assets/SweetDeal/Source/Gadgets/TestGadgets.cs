using System;
using SweetDeal.Source.Gadgets.Inventory;
using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class TestGadgets : MonoBehaviour
    {
        [SerializeField] private ToolsBar  _toolsBar;

        [SerializeField] private float throwForce;
        [SerializeField] private float throwHeight;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform player;
        
        [SerializeField] private GrenadeProjectile grenadeProjectile;
        [SerializeField] private JarOil jarOil;
        [SerializeField] private GadgetDefinition[]  definitions;
        
        private void Awake()
        {
            _toolsBar.Fill(new FuneralGrenade(grenadeProjectile, spawnPoint, throwHeight, throwForce, 2, definitions[0]));
            _toolsBar.Fill(new Oil(jarOil, spawnPoint, throwHeight, throwForce, player, 2, definitions[1]));
        }
    }
}