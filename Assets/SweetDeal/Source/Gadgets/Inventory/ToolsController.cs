using UnityEngine;
using UnityEngine.InputSystem;

namespace SweetDeal.Source.Gadgets.Inventory
{
    public class ToolsController : MonoBehaviour
    {
        [SerializeField] private ToolsBar toolsBar;
        [SerializeField] private ToolsBarView toolsBarView;

        public void Init(PCInput input)
        {
            input.Player.InventoryChoose.started += BindSwap;
            input.Player.Gadget.started += BindGadget;
        }

        private void BindSwap(InputAction.CallbackContext context)
        {
            toolsBar.Swap();
        }

        private void BindGadget(InputAction.CallbackContext context)
        {
            toolsBar.Use();
        }
        
    }
}