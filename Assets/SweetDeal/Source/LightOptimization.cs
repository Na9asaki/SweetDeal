using System;
using System.Collections;
using UnityEngine;

namespace SweetDeal.Source
{
    public class LightOptimization : MonoBehaviour
    {
        [SerializeField] private Light[]  lights;
        [SerializeField] private float timeToTurn;

        private Coroutine _current;

        private void Awake()
        {
            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }

        private IEnumerator TurnLightRoutine(bool active)
        {
            var duration = new WaitForSeconds(timeToTurn);
            foreach (var light in lights)
            {
                light.enabled = active;
                yield return duration;
            }
            _current = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = StartCoroutine(TurnLightRoutine(true));
        }

        private void OnTriggerExit(Collider other)
        {
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = StartCoroutine(TurnLightRoutine(false));
        }
    }
}