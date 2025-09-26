using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator.Configs
{
    [CreateAssetMenu(fileName = "LocationGeneratorConfig", menuName = "SweetDeal/LocationGeneratorConfig")]
    public class LocationScriptableObject : ScriptableObject
    {
        [field: SerializeField] public List<Room> NoDoors { get; private set; }
        [field: SerializeField] public List<Room> OneAndMoreDoors { get; private set; }
        [field: SerializeField] public List<Room> Hub { get; private set; }
        [field: SerializeField] public Room Exit {get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}