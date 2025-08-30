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
        [SerializeField] private Animator animator;
        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            #if UNITY_EDITOR
            DataKeeper.Save(new CookieEquipmentData()
            {
                cookies = 16
            }, STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);
            #endif
            
            FindAnyObjectByType<PlayerCoinsView>().Init();
            
            playerContainer.Init();
            playerEquipmentAndImprovementStatus.Init();
            var cookieData = DataKeeper.Load<CookieEquipmentData>(STRING_KEYS_CONSTRAINTS.CookieEquipmentKey, true);
            if (cookieData != null)
            {
                playerCoins.Add(cookieData.cookies);
            }
            
            shop.Activate();
            shopPanel.SetActive(false);

            if (PlayerPrefs.GetInt(STRING_KEYS_CONSTRAINTS.PlayerDeadKey, 0) == 1)
            {
                animator.SetTrigger("WakeUp");
                PlayerPrefs.SetInt(STRING_KEYS_CONSTRAINTS.PlayerDeadKey, 0);
                PlayerPrefs.Save();
            }
        }

        public void Raid()
        {
            LevelLoader.LoadGameplay();
        }
    }
}