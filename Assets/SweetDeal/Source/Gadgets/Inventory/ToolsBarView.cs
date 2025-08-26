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
        [SerializeField] private Sprite defaultSprite;
        
        private void OnEnable()
        {
            toolsBar.OnSelected += Swap;
        }

        private void OnDisable()
        {
            toolsBar.OnSelected -= Swap;
        }

        private void Swap(Gadget gadget)
        {
            if (gadget == null)
            {
                slots.sprite = defaultSprite;
                return;
            }

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