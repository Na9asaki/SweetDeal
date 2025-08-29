using System;
using SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements;
using SweetDeal.Source.Stealth;
using UnityEngine;
using Grid = SweetDeal.Source.LocationGenerator.Grid;

namespace SweetDeal.Source.AI
{
    public class AI : MonoBehaviour, INoiseListener
    {
        private BehaviourConstruct _construct;
        private Vector2Int _gridPosition;

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

        public void Alert(Vector3 soundPosition)
        {
            var soundGridPos = Grid.WorldToGrid(soundPosition);
            Debug.Log($"{soundGridPos} | {_gridPosition}");
            if (soundGridPos == _gridPosition)
            {
                _construct.Data.NoisePosition = soundPosition;
                _construct.BehaviourMachine.EnterIn<FollowState>();
            }
        }
    }
}