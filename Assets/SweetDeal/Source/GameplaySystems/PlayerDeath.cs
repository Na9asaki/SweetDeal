using System.Collections;
using SweetDeal.Source.Player;
using SweetDeal.Source.Scenes;
using UnityEngine;
using UnityEngine.Events;

namespace SweetDeal.Source.GameplaySystems
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private float timeToDeath = 5;

        public UnityEvent onDeathStarted;
        
        private PlayerController _playerController;

        private IEnumerator Death()
        {
            onDeathStarted.Invoke();
            yield return new WaitForSeconds(timeToDeath);
            LevelLoader.LoadMenu();
        }

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void Die()
        {
            _playerController.Deactivate();
            PlayerPrefs.SetInt(STRING_KEYS_CONSTRAINTS.PlayerDeadKey, 1);
            PlayerPrefs.Save();
            DataKeeper.DeleteKey(STRING_KEYS_CONSTRAINTS.CookieEquipmentKey);
            DataKeeper.DeleteKey(STRING_KEYS_CONSTRAINTS.EquipmentKey);
            DataKeeper.DeleteKey(STRING_KEYS_CONSTRAINTS.PlayerCoinsKey);
            DataKeeper.DeleteKey(STRING_KEYS_CONSTRAINTS.HeroImproveKey);
            StartCoroutine(Death());
        }
    }
}