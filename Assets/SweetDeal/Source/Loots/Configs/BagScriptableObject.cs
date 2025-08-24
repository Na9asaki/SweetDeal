using UnityEngine;

[CreateAssetMenu(fileName = "BagScriptableObject", menuName = "Scriptable Objects/BagScriptableObject")]
public class BagScriptableObject : ScriptableObject
{
    [field: SerializeField] public int Capacity {get; set;}
    
}
