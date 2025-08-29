using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class AnimationThrowGadgetEvent : MonoBehaviour
    {
        [SerializeField] private Transform gadgetPoint;

        private Projectile _projectile;
        private Vector3 _velocity;
        private Rigidbody _rigidbody;

        public void Throw()
        {
            _projectile = gadgetPoint.GetComponentInChildren<Projectile>();
            _rigidbody = _projectile.GetComponent<Rigidbody>();
            _velocity = _rigidbody.linearVelocity;
            _rigidbody.isKinematic = true;
        }
        
        public void LetGo()
        {
            gadgetPoint.GetComponentInChildren<Projectile>().transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.linearVelocity = _velocity;
        }
    }
}