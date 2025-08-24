using UnityEngine;

namespace SweetDeal.Source.LocationGenerator
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool _canOpen;
        
        public bool CanOpen => _canOpen;
        
        public Vector3 Position => transform.position;

        public void Activate()
        {
            _canOpen = true;
        }

        public void Open()
        {
            if (_canOpen)
                Debug.Log("Open");
            else 
                Debug.Log("Close");
        }
    }
}