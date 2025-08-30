using SweetDeal.Source.GameplaySystems;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.AI
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private AnimationProvider animationProvider;
        [SerializeField] private AI ai;
        [SerializeField] private AnimationHandGrab animationHandGrab;
        
        public UnityEvent onPlayerKilled;
        private void OnTriggerEnter(Collider other)
        {
            animationHandGrab.Init(other.GetComponent<PlayerDeath>());
            ai.Stop();
            transform.parent.forward = (other.transform.position - transform.parent.position);
            animationProvider.Kill();
            other.GetComponent<PlayerDeath>().Die();
            onPlayerKilled.Invoke();
        }
    }
}