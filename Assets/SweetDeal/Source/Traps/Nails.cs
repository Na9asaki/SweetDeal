using System.Collections;
using SweetDeal.Source.Stealth;
using UnityEngine;

namespace SweetDeal.Source.Traps
{
    [RequireComponent(typeof(NoiseSignal))]
    public class Nails : Trap
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string triggerName;

        [SerializeField] private Rigidbody[] _nails;
        [SerializeField] private Vector2 noiseDelayRange;
        [SerializeField] private float noisePower;
        [SerializeField] private int countNoiseEmit = 3;
        
        private NoiseSignal _noiseSignal;

        private void Awake()
        {
            _noiseSignal = GetComponent<NoiseSignal>();
        }

        private IEnumerator EmitRoutine()
        {
            int count = countNoiseEmit;

            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(Random.Range(noiseDelayRange.x, noiseDelayRange.y));
                _noiseSignal.Emit(transform.position, noisePower);
            }
        }

        protected override void Activate()
        {
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger(triggerName);
            foreach (var body in _nails)
            {
                body.isKinematic = false;
            }
            StartCoroutine(EmitRoutine());
        }
    }
}