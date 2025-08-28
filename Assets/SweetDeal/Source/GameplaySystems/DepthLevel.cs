using SweetDeal.Source.LocationGenerator.Configs;
using UnityEngine;

namespace SweetDeal.Source.GameplaySystems
{
    public class DepthLevel : MonoBehaviour
    {
        [SerializeField] private LocationScriptableObject[] locationsDefinitions;

        private int _depthLevel = 0;
        
        public int Level => _depthLevel;

        public LocationScriptableObject GetLocationDefinition()
        {
            return locationsDefinitions[_depthLevel];
        }

        public void UpdateValue()
        {
            _depthLevel = (_depthLevel + 1) % locationsDefinitions.Length;
        }
    }
}