using System.Collections.Generic;
using SweetDeal.Source.LocationGenerator;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Door[] _exites;
    [SerializeField] private Door _entry;
    [SerializeField] private Transform _center;
    [field: SerializeField] public float Radius { get; private set; }
    
    public IEnumerable<Door> Exites => _exites;
    public Door Entry => _entry;
    
    public bool CanPlace => Physics.OverlapSphere(_center.position, Radius).Length == 0;
    
    public void GenerateLoot()
    {
        Debug.Log("Generating Loot...");
    }

}
