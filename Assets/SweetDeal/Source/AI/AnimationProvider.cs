using System;
using UnityEngine;
using UnityEngine.AI;

namespace SweetDeal.Source.AI
{
    public class AnimationProvider : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private string velocityParameter = "Velocity";
        [SerializeField] private string killParameter = "Kill";
        [SerializeField] private string slipParameter = "Slip";
        
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            animator.SetFloat(velocityParameter, _agent.velocity.sqrMagnitude);
        }

        public void Kill()
        {
            animator.SetTrigger(killParameter);
        }

        public void Slip()
        {
            animator.SetTrigger(slipParameter);
        }
    }
}