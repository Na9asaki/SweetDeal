using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.LocationGenerator.Configs;
using SweetDeal.Source.Player;
using UnityEngine;

namespace SweetDeal.Source
{
    public class Boostrap : MonoBehaviour
    {
        [SerializeField] private PlayerController  _playerController;
        [SerializeField] private LocationGenerator.LocationGenerator _locationGenerator;
        [SerializeField] private Door _startDoor;
        [SerializeField] private LocationScriptableObject  _locationScriptableObject;
        
        private PCInput _input;

        private void Awake()
        {
            _input = new PCInput();
            _playerController.Init(_input);
            _locationGenerator.Generate(_locationScriptableObject, _startDoor);
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