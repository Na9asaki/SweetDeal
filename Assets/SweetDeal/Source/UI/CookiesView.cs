using System;
using SweetDeal.Source.Loots;
using TMPro;
using UnityEngine;

namespace SweetDeal.Source.UI
{
    public class CookiesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text cookieText;
        [SerializeField] private Cargo cargo;

        private void UpdateView()
        {
            cookieText.text = $"x {cargo.Count}";
        }
        
        private void OnEnable()
        {
            cargo.OnAdded += UpdateView;
        }

        private void OnDisable()
        {
            cargo.OnAdded -= UpdateView;
        }
    }
}