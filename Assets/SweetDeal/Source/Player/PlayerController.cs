using System;
using StarterAssets;
using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ThirdPersonController  _thirdPersonController;
        [SerializeField] private PlayerInteraction _playerInteraction;

        public void Init(PCInput input)
        {
            _playerInteraction.Init(input);
        }
        
        public void Activate()
        {
            _thirdPersonController.enabled = true;
        }

        public void Deactivate()
        {
            _thirdPersonController.enabled = false;
        }
    }
}