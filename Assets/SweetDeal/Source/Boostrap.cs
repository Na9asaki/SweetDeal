using SweetDeal.Source.Gadgets.Inventory;
using SweetDeal.Source.GameplaySystems;
using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Player;
using UnityEngine;

namespace SweetDeal.Source
{
    public class Boostrap : MonoBehaviour
    {
        [SerializeField] private PlayerController  _playerController;
        [SerializeField] private LocationGenerator.LocationGenerator _locationGenerator;
        [SerializeField] private Door _startDoor;
        [SerializeField] private DepthLevel _depthLevel;
        [SerializeField] private DataEntryPoint _dataEntryPoint;
        
        [SerializeField] private ToolsController _toolsController;
        
        private PCInput _input;
        private Vector3 _spawnPoint;
        
        public DepthLevel DepthLevel => _depthLevel;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _input = new PCInput();
            _playerController.Init(_input);
            _toolsController.Init(_input);
            _spawnPoint = _playerController.transform.position;
            
            _dataEntryPoint.Load();
        }

        private void Start()
        {
            Run();
        }

        private void Run()
        {
            var definition = _depthLevel.GetLocationDefinition();
            
            _locationGenerator.Restart();
            
            _locationGenerator.Generate(definition, _startDoor);

            _input.Enable();
            _playerController.Activate();
        }

        public void Restart()
        {
            _depthLevel.UpdateValue();
            _playerController.GetComponent<CharacterController>().enabled = false;
            _playerController.transform.position = _spawnPoint;
            _playerController.GetComponent<CharacterController>().enabled = true;
            Run();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}