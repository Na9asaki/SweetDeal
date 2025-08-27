using SweetDeal.Source.GameplaySystems;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.AI
{
    public class DeadZone : MonoBehaviour
    {
        public UnityEvent onPlayerKilled;
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerDeath>().Die();
            onPlayerKilled.Invoke();
        }
    }
}