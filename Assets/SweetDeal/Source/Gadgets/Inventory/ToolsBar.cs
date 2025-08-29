using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.Gadgets.Inventory
{
    public class ToolsBar : MonoBehaviour
    {
        [SerializeField] private float cooldown = 2f;
        
        private Gadget[] _gadgets = new Gadget[2];
        
        private int _selected;
        private float lastTimeUse = 0;
        
        public event Action<Gadget> OnSelected;
        public event Action<Gadget> OnGrenadeThrowed;
        
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
                if (lastTimeUse <= Time.time)
                {
                    _gadgets[_selected].Use();
                    OnGrenadeThrowed?.Invoke(_gadgets[_selected]);
                    if (_gadgets[_selected].IsEmpty)
                    {
                        _gadgets[_selected] = null;
                        OnSelected?.Invoke(null);
                    }
                    lastTimeUse = Time.time + cooldown;
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