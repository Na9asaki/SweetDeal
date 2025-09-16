using SweetDeal.Source.AI.BehaviourStateMachine.StatesImplements;
using SweetDeal.Source.Player;
using SweetDeal.Source.Stealth;
using UnityEngine;

namespace SweetDeal.Source.AI
{
    public class RaycastNoiseTrigger : MonoBehaviour, INoiseListener
    {
        [SerializeField] private BehaviourConstruct construct;
        [SerializeField] private LayerMask layerMask;
        
        public void Alert(Vector3 soundPosition)
        {
            var direction = soundPosition - transform.position;
            if (!Physics.Raycast(transform.position, direction, 100, layerMask))
            {
                construct.Data.NoisePosition = soundPosition;
                construct.BehaviourMachine.EnterIn<FollowState>();
            }
        }
    }
}