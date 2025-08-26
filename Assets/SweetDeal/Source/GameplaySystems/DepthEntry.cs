using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Loots;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.GameplaySystems
{
    public class DepthEntry : MonoBehaviour
    {
        [SerializeField] private int Price = 10;
        [SerializeField] private float LevelModifier = 1;
        
        public UnityEvent onLoadNextLevelDepth;

        private Boostrap _boostrap;
        private Door _door;
        private Cargo _cargo;

        private void Awake()
        {
            _boostrap = FindAnyObjectByType<Boostrap>();
            _cargo = FindAnyObjectByType<Cargo>();

            Price = Mathf.RoundToInt(_boostrap.DepthLevel.Level * LevelModifier * Price);
            
            _door = GetComponent<Door>();
            
            _door.onInteractionUsedUnityEvent.AddListener(Enter);
        }
        
        private void Enter()
        {
            if (!_cargo.Spend(Price))
            {
                return;
            }
            
            onLoadNextLevelDepth.Invoke();
            _boostrap.Restart();
        }
    }
}