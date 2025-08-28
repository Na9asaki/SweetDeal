using System;
using System.Collections;
using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.Stealth
{
    [RequireComponent(typeof(NoiseSignal))]
    public class StepNoise : MonoBehaviour
    {
        [SerializeField] CharacterController _controller;
        [SerializeField] private Cargo _cargo;
        [SerializeField] private float _noiseModifier;
        [SerializeField] private float _noiseDuration;

        private Coroutine _coroutine;
        private NoiseSignal _noiseSignal;

        private void Awake()
        {
            _noiseSignal = GetComponent<NoiseSignal>();
        }

        public void SetNoiseModifier(float noiseModifier)
        {
            _noiseModifier *= (1 - noiseModifier);
        }

        private void FixedUpdate()
        {
            if (_controller.velocity.sqrMagnitude > 0.01f)
            {
                if (_coroutine == null) Activate();
            } else if (_coroutine != null) Deactivate();
        }

        private IEnumerator StepNoiseRoutine(float power)
        {
            while (true)
            {
                _noiseSignal.Emit(transform.position, power);
                yield return new WaitForSeconds(_noiseDuration);
            }
        }
        
        private void Activate()
        {
            float power = 0;
            int capacity = 0;
            foreach (var bag in _cargo.Bags)
            {
                capacity += bag.Capacity;
                power += bag.Count;
            }

            power /= capacity;
            power *= _noiseModifier;
            
            _coroutine = StartCoroutine(StepNoiseRoutine(power));
        }

        private void Deactivate()
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

    }
}