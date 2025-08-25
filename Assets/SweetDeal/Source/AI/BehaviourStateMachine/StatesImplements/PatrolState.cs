using UnityEngine;
using UnityEngine.AI;

namespace SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements
{
    public class PatrolState : IState
    {
        private readonly Vector3[] _patrolPositions;
        private readonly NavMeshAgent _agent;
        
        private int _index;
        private Vector3 _nextPoint;

        private float _duration = 0;
        private float _timeToStop;

        public PatrolState(NavMeshAgent agent, Vector3[] patrolPositions, float timeToStop)
        {
            _patrolPositions = patrolPositions;
            _agent = agent;
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
            _nextPoint = _patrolPositions[_index];
            _index++;
            
            _agent.SetDestination(_nextPoint);
        }

        public void Exit()
        {
            _duration = 0;
        }

        public void Update()
        {
            if (_agent.remainingDistance <=  _agent.stoppingDistance)
            {
                if (!Wait())
                {
                    _nextPoint = _patrolPositions[_index];
                    _index = (_index + 1) % _patrolPositions.Length;
                    _agent.SetDestination(_nextPoint);
                }
            }
        }
    }
}