using UnityEngine;
using UnityEngine.AI;

namespace SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements
{
    public class FollowState : IState
    {
        private NoiseData _data;
        private NavMeshAgent _agent;
        
        private BehaviourMachine _behaviour;

        private float _duration;
        private float _timeToStop;

        private Vector3 oldPostion;

        public FollowState(NoiseData data,  NavMeshAgent agent,  BehaviourMachine behaviour, float timeToStop)
        {
            _data = data;
            _agent = agent;
            _behaviour = behaviour;
            _timeToStop = timeToStop;
        }

        private bool Wait()
        {
            if (_duration < _timeToStop)
            {
                _duration += Time.deltaTime;
                return true;
            }

            _duration = 0;
            return false;
        }
        
        public void Enter()
        {
            oldPostion = _data.NoisePosition;
            _agent.SetDestination(_data.NoisePosition);
        }

        public void Exit()
        {
            _duration = 0;
        }

        public void Update()
        {
            if (_data.NoisePosition != oldPostion)
            {
                _agent.SetDestination(_data.NoisePosition);
                oldPostion = _data.NoisePosition;
            }

            if (_agent.remainingDistance <=  _agent.stoppingDistance)
            {
                if (!Wait())
                {
                    _behaviour.EnterIn<PatrolState>();
                }
            }
        }
    }
}