using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.Stealth
{
    public class NoiseSignal : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        
        private Collider[] _colliders =  new Collider[4];

        // можно подписаться, чтобы воспроизвести звук
        public UnityEvent onEmit;

        public void Emit(Vector3 position, float force)
        {
            var listenersCount = Physics.OverlapSphereNonAlloc(position, force, _colliders, _layerMask);
            for (int i = 0; i < listenersCount; i++)
            {
                _colliders[i].GetComponent<INoiseListener>().Alert(position);
            }
        }
    }
}