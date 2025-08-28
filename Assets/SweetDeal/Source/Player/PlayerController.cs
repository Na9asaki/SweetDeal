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

        private IEnumerator StunRoutine(float timeStun)
        {
            float baseSpeed = _thirdPersonController.MoveSpeed;
            _thirdPersonController.MoveSpeed = 0;
            yield return new WaitForSeconds(timeStun);
            _thirdPersonController.MoveSpeed = baseSpeed;
        }

        public void Stun(float timeStun)
        {
            _playerAnimatorProvider.Slip();
            StartCoroutine(StunRoutine(timeStun));
        }
    }
}