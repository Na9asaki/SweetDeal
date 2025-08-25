using System;
using SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements;
using SweetDeal.Source.Stealth;
using UnityEngine;

namespace SweetDeal.Source.AI
{
    public class AI : MonoBehaviour, INoiseListener
    {
        private BehaviourConstruct _construct;

        private void Awake()
        {
            _construct = GetComponent<BehaviourConstruct>();
        }

        private void Start()
        {
            _construct.BehaviourMachine.EnterIn<PatrolState>();
        }

        private void FixedUpdate()
        {
            _construct.BehaviourMachine.Update();
        }

        public void Alert(Vector3 soundPosition)
        {
            _construct.Data.NoisePosition = soundPosition;
            _construct.BehaviourMachine.EnterIn<FollowState>();
        }
    }
}