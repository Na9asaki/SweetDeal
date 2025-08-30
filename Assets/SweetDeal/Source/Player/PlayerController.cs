using System.Collections;
using StarterAssets;
using SweetDeal.Source.Gadgets;
using UnityEngine;

namespace SweetDeal.Source.Player
{
    public class PlayerController : MonoBehaviour, IStunned
    {
        [SerializeField] private ThirdPersonController  _thirdPersonController;
        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private PlayerAnimatorProvider _playerAnimatorProvider;
        [SerializeField] private CharacterController _characterController;
        
        private Coroutine _coroutine;
        
        public void Init(PCInput input)
        {
            _playerInteraction.Init(input);
        }

        public void SpeedOff()
        {
            _thirdPersonController.SprintSpeed = 0f;
            _thirdPersonController.MoveSpeed = 0f;
            GetComponent<CharacterController>().enabled = false;
        }
        
        public void Activate()
        {
            _thirdPersonController.enabled = true;
        }

        public void Deactivate()
        {
            _thirdPersonController.enabled = false;
        }

        private IEnumerator StunRoutine(float timeStun)
        {
            float baseSpeed = _thirdPersonController.MoveSpeed;
            float baseRunSpeed = _thirdPersonController.SprintSpeed;
            _thirdPersonController.MoveSpeed = 0;
            _thirdPersonController.SprintSpeed = 0;
            yield return new WaitForSeconds(timeStun);
            _thirdPersonController.MoveSpeed = baseSpeed;
            _thirdPersonController.SprintSpeed = baseRunSpeed;
            _coroutine = null;
        }

        public void Stun(float timeStun)
        {
            _playerAnimatorProvider.Slip();
            if (_coroutine == null)
                _coroutine = StartCoroutine(StunRoutine(timeStun));
        }
    }
}