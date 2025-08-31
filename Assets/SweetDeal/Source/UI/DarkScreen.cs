using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SweetDeal.Source.UI
{
    public class DarkScreen : MonoBehaviour
    {
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private Image image;
        [SerializeField] private float duration = 2f;

        private IEnumerator ShowDarkScreenRoutine()
        {
            float t = 0;
            while (t <= duration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / duration;
                image.color = new Color(0, 0 ,0, animationCurve.Evaluate(normalizedTime));
                yield return null;
            }
        }
        
        public void Activate()
        {
            StartCoroutine(ShowDarkScreenRoutine());
        }
    }
}