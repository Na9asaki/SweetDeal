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
        [SerializeField] private float maxSqrSpeed = 10;
        [SerializeField] private Cargo _cargo;
        [SerializeField] private float _noiseModifier;
        [SerializeField] private float _noiseDuration;
        [SerializeField] private float _baseMinimalNouse = 5f;

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

        private IEnumerator StepNoiseRoutine(float capacityPower)
        {
            float savedCapacity = capacityPower;
            while (true)
            {
                capacityPower = savedCapacity * _noiseModifier + _baseMinimalNouse;
                capacityPower *= _controller.velocity.sqrMagnitude / maxSqrSpeed;
                _noiseSignal.Emit(transform.position, capacityPower);
                yield return new WaitForSeconds(_noiseDuration);
            }
        }
        
        private void Activate()
        {
            float power = 0;
            int capacity = _cargo.Capacity;
            power += _cargo.Count;

            power /= capacity;
            _coroutine = StartCoroutine(StepNoiseRoutine(power));
        }

        private void Deactivate()
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

    }
}