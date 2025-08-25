using System;
using System.Collections;
using UnityEngine;

namespace SweetDeal.Source.Loots
{
    [RequireComponent(typeof(Interaction))]
    public class InteractionView : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private Interaction _loot;

        private bool _activated;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _loot = GetComponent<Interaction>();
        }

        private void OnEnable()
        {
            _loot.OnLootEntered += Activate;
            _loot.OnLootExited += Deactivate;
        }

        private void OnDisable()
        {
            _loot.OnLootEntered -= Activate;
            _loot.OnLootExited -= Deactivate;
        }

        private IEnumerator ViewRoutine()
        {
            while (_activated)
            {
                _view.transform.LookAt(_camera.transform.position);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
        public void Activate()
        {
            _activated = true;
            _view.SetActive(true);
            StartCoroutine(ViewRoutine());
        }

        public void Deactivate()
        {
            _activated = false;
            _view.SetActive(false);
        }
    }
}