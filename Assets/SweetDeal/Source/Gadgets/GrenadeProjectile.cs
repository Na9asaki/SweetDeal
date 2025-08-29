using System.Collections;
using SweetDeal.Source.Stealth;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.Gadgets
{
    public class GrenadeProjectile : Projectile
    {
        [SerializeField] private float timeToExplode;
        [SerializeField] private float noiseVolume;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private GameObject explosionEffect;
        private NoiseSignal _noiseSignal;
        
        public UnityEvent onExplode;

        protected override void Awake()
        {
            base.Awake();
            _noiseSignal = GetComponent<NoiseSignal>();
        }

        private IEnumerator ExplodeRoutine()
        {
            yield return new WaitForSeconds(timeToExplode);
            explosionEffect.transform.position = transform.position;
            onExplode.Invoke();
            _rigidbody.isKinematic = true;
            _noiseSignal.Emit(transform.position, noiseVolume);
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(timeToExplode);
            Destroy(gameObject);
        }

        protected override void Do()
        {
            StartCoroutine(ExplodeRoutine());
        }
    }
}