using System;
using System.Collections;
using SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements;
using SweetDeal.Source.Gadgets;
using SweetDeal.Source.Stealth;
using UnityEngine;
using UnityEngine.AI;
using Grid = SweetDeal.Source.LocationGenerator.Grid;

namespace SweetDeal.Source.AI
{
    public class AI : MonoBehaviour, IStunned
    {
        [SerializeField] private DeadZone deadZone;
        [SerializeField] private AnimationProvider animationProvider;
        
        private BehaviourConstruct _construct;
        private Vector2Int _gridPosition;
        
        private Coroutine _coroutine;

        private void Awake()
        {
            _construct = GetComponent<BehaviourConstruct>();
        }

        private void Start()
        {
            _construct.BehaviourMachine.EnterIn<PatrolState>();
            _gridPosition = Grid.WorldToGrid(transform.position);
        }

        private void FixedUpdate()
        {
            _construct.BehaviourMachine.Update();
        }

        public void Stop()
        {
            _construct.BehaviourMachine.Exit();
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        private IEnumerator StunRoutine(float time)
        {
            deadZone.GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshAgent>().isStopped = true;
            animationProvider.Slip();
            
            yield return new WaitForSeconds(time);
            
            deadZone.GetComponent<Collider>().enabled = true;
            GetComponent<NavMeshAgent>().isStopped = false;
            _coroutine = null;
        }

        public void Stun(float timeStun)
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(StunRoutine(timeStun));
            }
        }
    }
}