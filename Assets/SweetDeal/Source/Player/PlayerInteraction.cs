using System;
using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        private Interaction _current;
        
        public void Init(PCInput input)
        {
            input.Player.Interaction.started += (ctx) => Interact();
        }

        private void OnEnable()
        {
            Interaction.OnInteractionEntered += Entered;
        }

        private void OnDisable()
        {
            Interaction.OnInteractionEntered -= Entered;
        }

        private void Entered(Interaction obj)
        {
            _current = obj;
            _current.OnLootExited += Exited;
        }

        private void Exited()
        {
            _current = null;
        }

        private void Interact()
        {
            if (_current == null) return;
            _current.Interact();
        }
    }
}