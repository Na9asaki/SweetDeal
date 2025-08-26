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
            onExplode.Invoke();
            _noiseSignal.Emit(transform.position, noiseVolume);
            Destroy(gameObject);
        }

        protected override void Do()
        {
            StartCoroutine(ExplodeRoutine());
        }
    }
}