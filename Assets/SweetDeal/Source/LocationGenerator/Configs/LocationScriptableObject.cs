using UnityEngine;

namespace SweetDeal.Source.LocationGenerator.Configs
{
    [CreateAssetMenu(fileName = "LocationGeneratorConfig", menuName = "SweetDeal/LocationGeneratorConfig")]
    public class LocationScriptableObject : ScriptableObject
    {
        [field: SerializeField] public Room[] NoDoors { get; private set; }
        [field: SerializeField] public Room[] OneAndMoreDoors { get; private set; }
        [field: SerializeField] public Room[] Hub { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}