using System;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class LootProvider : MonoBehaviour
    {
        [SerializeField] private Cargo _cargo;
        private Loot _current;
        
        public void Init(PCInput input)
        {
            input.Player.Interaction.started += (ctx) => Interact();
        }

        private void OnEnable()
        {
            Loot.OnLootCollected += Enter;
        }

        private void OnDisable()
        {
            Loot.OnLootCollected -= Enter;
        }

        private void Interact()
        {
            _cargo.Fill(_current.Collect());
        }

        private void Enter(Loot loot)
        {
            _current = loot;
            _current.OnLootExited += Exit;
        }

        private void Exit()
        {
            _current.OnLootExited -= Exit;
            _current = null;
        }
    }
}