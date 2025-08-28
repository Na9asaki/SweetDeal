using SweetDeal.Source.Stealth;
using UnityEngine;

namespace SweetDeal.Source.Traps
{
    [RequireComponent(typeof(NoiseSignal))]
    public class Gargoyle : Trap
    {
        [SerializeField] private float noisePower;
        
        private  NoiseSignal _noiseSignal;

        private void Awake()
        {
            _noiseSignal = GetComponent<NoiseSignal>();
        }

        protected override void Activate()
        {
            _noiseSignal.Emit(transform.position, noisePower);
        }
    }
}