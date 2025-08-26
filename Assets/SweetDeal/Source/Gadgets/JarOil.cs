using System;
using UnityEngine;

namespace SweetDeal.Source.Gadgets
{
    public class JarOil : Projectile
    {
        [SerializeField] PuddleOil puddleOil;
        
        private void OnCollisionEnter(Collision other)
        {
            var paddle = Instantiate(puddleOil, transform.position, Quaternion.identity);
            paddle.transform.parent = other.transform;
            Destroy(gameObject);
        }

        protected override void Do()
        {
            
        }
    }
}