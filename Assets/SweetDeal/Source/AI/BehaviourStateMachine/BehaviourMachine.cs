using System;
using System.Collections.Generic;
using SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements;
using UnityEngine;
using UnityEngine.AI;

namespace SweetDeal.Source.AI.BehaviourStateMachine
{
    public class BehaviourMachine
    {
        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

        private IState _currentState;
        
        public BehaviourMachine(
            NavMeshAgent agent,
            Vector3[] waypoints,
            NoiseData noiseData,
            float timeToStop)
        {
            _states[typeof(PatrolState)] = new PatrolState(agent, waypoints, timeToStop);
            _states[typeof(FollowState)] = new FollowState(noiseData, agent, this, timeToStop);
        }

        public void EnterIn<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void Exit()
        {
            _currentState?.Exit();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}