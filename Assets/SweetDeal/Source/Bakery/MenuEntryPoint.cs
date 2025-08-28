using SweetDeal.Source.Bakery.ShopSystem;
using SweetDeal.Source.GameplaySystems;
using SweetDeal.Source.Scenes;
using UnityEngine;

namespace SweetDeal.Source.Bakery
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerEquipmentAndImprovementStatus  playerEquipmentAndImprovementStatus;
        [SerializeField] private PlayerCoins playerCoins;
        [SerializeField] private Shop shop;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private PlayerContainer playerContainer;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            #if UNITY_EDITOR
            DataKeeper.Save(new CookieEquipmentData()
            {
                cookies = 100
            }, STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);
            #endif
            
            playerContainer.Init();
            playerEquipmentAndImprovementStatus.Init();
            var cookieData = DataKeeper.Load<CookieEquipmentData>(STRING_KEYS_CONSTRAINTS.CookieEquipmentKey, true);
            if (cookieData != null)
            {
                playerCoins.Add(cookieData.cookies);
            }
            shop.Activate();
            shopPanel.SetActive(false);
        }

        public void Raid()
        {
            //playerContainer.Save();
            LevelLoader.LoadGameplay();
        }
    }
}