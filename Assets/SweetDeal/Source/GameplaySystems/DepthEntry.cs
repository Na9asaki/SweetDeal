using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Loots;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.GameplaySystems
{
    public class DepthEntry : MonoBehaviour
    {
        [SerializeField] private int Price = 10;
        [SerializeField] private float LevelModifier = 1;
        [SerializeField] private TMP_Text _priceView;
        
        public UnityEvent onLoadNextLevelDepth;

        private Boostrap _boostrap;
        private Door _door;
        private Cargo _cargo;

        private void Start()
        {
            _boostrap = FindAnyObjectByType<Boostrap>();
            _cargo = FindAnyObjectByType<Cargo>();

            Price = Mathf.RoundToInt((_boostrap.DepthLevel.Level + 1) * LevelModifier * Price);
            
            _door = GetComponent<Door>();
            
            _door.onInteractionUsedUnityEvent.AddListener(Enter);

            _priceView.text = $"Cost: {Price}";
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