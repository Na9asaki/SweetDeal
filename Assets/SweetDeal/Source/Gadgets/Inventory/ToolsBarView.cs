using System;
using SweetDeal.Source.Gadgets.Inventory.SpriteConfigurable;
using UnityEngine;
using UnityEngine.UI;

namespace SweetDeal.Source.Gadgets.Inventory
{
    public class ToolsBarView : MonoBehaviour
    {
        [SerializeField] private Image slots;
        [SerializeField] private GadgetDefinition[] _views;
        [SerializeField] private ToolsBar toolsBar;
        
        private Color _initialColor;
        
        private void OnEnable()
        {
            toolsBar.OnSelected += Swap;
        }

        private void OnDisable()
        {
            toolsBar.OnSelected -= Swap;
        }

        private void Awake()
        {
            _initialColor = slots.color;
        }

        private void Swap(Gadget gadget)
        {
            if (gadget == null)
            {
                slots.sprite = null;
                slots.color = Color.clear;
                return;
            }

            slots.color = _initialColor;
            foreach (var view in _views)
            {
                if (gadget.Definition == view)
                {
                    slots.sprite = view.Sprite;
                    break;
                }
            }
        }
    }
}