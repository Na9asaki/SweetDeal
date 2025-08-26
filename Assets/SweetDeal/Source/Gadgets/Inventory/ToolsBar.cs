using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Gadgets.Inventory
{
    public class ToolsBar : MonoBehaviour
    {   
        private Gadget[] _gadgets = new Gadget[2];
        
        private int _selected;
        
        public event Action<Gadget> OnSelected;
        
        public IEnumerable<Gadget> Gadgets => _gadgets;

        private void Start()
        {
            Swap();
        }

        public void Swap(int slotNumber)
        {
            _selected = slotNumber;
        }
        public void Swap()
        {
            for (int i = 1; i < _gadgets.Length; i++)
            {
                var newSelected = (_selected + 1) % _gadgets.Length;
                if (_gadgets[newSelected] != null)
                {
                    _selected = newSelected;
                    break;
                }
            }
            OnSelected?.Invoke(_gadgets[_selected]);
        }

        public void Use()
        {
            if (_gadgets[_selected] != null)
            {
                _gadgets[_selected].Use();
                if (_gadgets[_selected].IsEmpty)
                {
                    _gadgets[_selected] = null;
                    OnSelected?.Invoke(null);
                }
            }
        }

        public void Fill(Gadget gadget)
        {
            for (int i = 0; i < _gadgets.Length; i++)
            {
                if (_gadgets[i] == null)
                {
                    _gadgets[i] = gadget;
                    break;
                }
            }
        }
    }
}