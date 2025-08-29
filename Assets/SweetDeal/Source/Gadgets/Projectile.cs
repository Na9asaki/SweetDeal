using System;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        protected Rigidbody _rigidbody;
        
        public Vector3 Direction { get; private set; }
        
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 direction, float force)
        {
            Direction = direction;
            _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
            Do();
        }

        protected abstract void Do();
    }
}