using SweetDeal.Source.GameplaySystems;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.AI
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private Transform handPoint;
        [SerializeField] private AnimationProvider animationProvider;
        
        public UnityEvent onPlayerKilled;
        private void OnTriggerEnter(Collider other)
        {
            animationProvider.Kill();
            other.GetComponent<PlayerDeath>().Die(handPoint);
            onPlayerKilled.Invoke();
        }
    }
}