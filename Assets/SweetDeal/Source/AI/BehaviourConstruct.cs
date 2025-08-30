using System.Linq;
using SweetDeal.Source.AI.BehaviourStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace SweetDeal.Source.AI
{
    public class BehaviourConstruct : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _timeToStop;
        
        public NoiseData Data {get; private set;}
        public BehaviourMachine BehaviourMachine {get; private set;}
        
        private Vector3[] Positions => _waypoints.Select(x => x.position).ToArray();
        
        public void Init()
        {
            GetComponent<NavMeshAgent>().enabled = true;
            Data = new NoiseData();

            BehaviourMachine = new BehaviourMachine(
                GetComponent<NavMeshAgent>(),
                Positions,
                Data,
                _timeToStop
            );
        }
    }
}