using UnityEngine;

namespace SweetDeal.Source.Loots
{
    public class LootProvider : MonoBehaviour
    {
        [SerializeField] private Cargo _cargo;

        private void OnEnable()
        {
            Loot.OnLootCollected += Supply;
        }

        private void OnDisable()
        {
            Loot.OnLootCollected -= Supply;
        }

        private void Supply(Loot loot)
        {
            _cargo.Fill(loot.Amount);
        }
    }
}