using System;
using SweetDeal.Source.Gadgets;
using SweetDeal.Source.Gadgets.Inventory;
using UnityEngine;

namespace SweetDeal.Source.Player
{
    public class PlayerAnimatorProvider : MonoBehaviour
    {
        [SerializeField] private string velocityParameter = "Velocity";
        [SerializeField] private string slipParameter = "Slip";
        [SerializeField] private string throwParameter = "Throw";
        [SerializeField] private string throwBackwardsParameter = "ThrowBackward";
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private ToolsBar toolsBar;
        
        private void Update()
        {
            animator.SetFloat(velocityParameter, characterController.velocity.sqrMagnitude);
        }

        private void OnEnable()
        {
            toolsBar.OnGrenadeThrowed += Throw;
        }

        private void OnDisable()
        {
            toolsBar.OnGrenadeThrowed -= Throw;
        }

        public void Idle()
        {
            animator.SetFloat(velocityParameter, 0);
        }

        public void Slip()
        {
            animator.SetTrigger(slipParameter);
        }

        private void Throw(Gadget gadget)
        {
            if (gadget is FuneralGrenade)
                animator.SetTrigger(throwParameter);
            else 
                animator.SetTrigger(throwBackwardsParameter);
        }
    }
}