using SweetDeal.Source.GameplaySystems;
using UnityEngine;

namespace SweetDeal.Source.AI
{
    public class AnimationHandGrab :  MonoBehaviour
    {
        [SerializeField] private Transform handPoint;

        private PlayerDeath _death;
        
        public void Init(PlayerDeath death)
        {
            _death = death;
        }

        public void Grab()
        {
            _death.transform.forward = transform.parent.parent.position - _death.transform.position;
            _death.Grab(handPoint);
        }
    }
}