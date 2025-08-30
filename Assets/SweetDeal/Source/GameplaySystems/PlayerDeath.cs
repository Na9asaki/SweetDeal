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
        [SerializeField] private PlayerAnimatorProvider animatorProvider;

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

        public void Grab(Transform parent)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void Die()
        {
            animatorProvider.Idle();
            animatorProvider.enabled = false;
            _playerController.SpeedOff();
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