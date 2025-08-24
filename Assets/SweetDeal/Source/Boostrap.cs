using System;
using SweetDeal.Source.Player;
using UnityEngine;

namespace SweetDeal.Source
{
    public class Boostrap : MonoBehaviour
    {
        [SerializeField] private PlayerController  _playerController;
        
        private PCInput _input;

        private void Awake()
        {
            _input = new PCInput();
            _playerController.Init(_input);
        }

        private void Start()
        {
            _input.Enable();
            _playerController.Activate();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}