using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SweetDeal.Source
{
    public class ForFun : MonoBehaviour
    {
        [SerializeField] private Volume volume;
        private ColorAdjustments _colorAdjustments;

        [SerializeField] private float bpm = 165f; // Примерный BPM Caramelldansen
        private float _timer;
        private float _interval;

        private void Awake()
        {
            if (volume != null && volume.profile != null)
            {
                volume.profile.TryGet(out _colorAdjustments);
            }

            // 1 удар = 60 / BPM
            _interval = 60f / bpm;
        }

        private void Update()
        {
            if (_colorAdjustments == null) return;

            _timer += Time.deltaTime;

            if (_timer >= _interval)
            {
                _timer = 0f;
                _colorAdjustments.hueShift.value = Random.Range(-180f, 180f);
            }
        }
    }
}