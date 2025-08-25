using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.Loots;
using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public class DungeonEntry : MonoBehaviour
    {
        private Door _door;
        private void Awake()
        {
            _door = GetComponent<Door>();
        }

        private void OnEnable()
        {
            _door.onInteractionUsedUnityEvent.AddListener(Exit);
        }

        private void OnDisable()
        {
            _door.onInteractionUsedUnityEvent.RemoveListener(Exit);
        }

        private void Exit()
        {
            var cargo = FindAnyObjectByType<Cargo>();
            int amount = 0;
            foreach (var bag in cargo.Bags)
            {
                amount += bag.Count;
            }
            CookieEquipmentData data = new()
            {
                cookies = amount
            };

            DataKeeper.Save(data, STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);
        }
    }
}