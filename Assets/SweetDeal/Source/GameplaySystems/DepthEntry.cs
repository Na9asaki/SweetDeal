using System;
using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Scenes;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.GameplaySystems
{
    public class DepthEntry : MonoBehaviour
    {
        public UnityEvent onLoadNextLevelDepth;

        private bool _lastDepthLevel;
        private Boostrap _boostrap;
        private Door _door;

        private void Awake()
        {
            _boostrap = FindAnyObjectByType<Boostrap>();
            _door = GetComponent<Door>();
            
            _door.onInteractionUsedUnityEvent.AddListener(Enter);
        }

        public void ChangeAction()
        {
            _lastDepthLevel = true;
        }
        
        private void Enter()
        {
            if (_lastDepthLevel)
            {
                LevelLoader.LoadMenu();
            }
            else
            {
                onLoadNextLevelDepth.Invoke();
                _boostrap.Restart();
            }
        }
    }
}