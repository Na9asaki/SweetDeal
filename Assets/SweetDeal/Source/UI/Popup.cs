using System;
using System.Collections;
using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.UI
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Message prefab;
        
        [SerializeField] private RectTransform spawnPoint;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Cargo cargo;

        [SerializeField] private float lifeTime = 1.2f;

        private IEnumerator ShowRoutine(Message msg)
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(msg.gameObject);
            
        }

        private void OnEnable()
        {
            cargo.OnAdded += Show;
        }

        private void OnDisable()
        {
            cargo.OnAdded -= Show;
        }

        private void Show(int amount)
        {
            var msg = Instantiate(prefab, canvas.transform);
            msg.Place(spawnPoint);
            msg.Write($"+{amount} Cookies");
            StartCoroutine(ShowRoutine(msg));
        }

    }
}