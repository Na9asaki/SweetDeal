using System.Collections.Generic;
using System.Linq;
using SweetDeal.Source.AI;
using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.LocationGenerator.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private Door[] _exites;
    [SerializeField] private Door _entry;
    [SerializeField] private BehaviourConstruct[] _ais;

    [SerializeField] private Transform[] _lootSpawnPoints;
    [SerializeField] private Transform[] _cookiesSpawnPoints;
    [SerializeField] private int _minCookies;
    [field: SerializeField] public BoxCollider Floor { get; private set; }
    [field: SerializeField] public Vector2Int DoorOffset { get; private set; }
    [field: SerializeField] public RoomCellsSO RoomCells { get; private set; }
    
    public IEnumerable<Door> Exites => _exites;
    public Door Entry => _entry;
    
    public void GenerateLoot(GeneratedLootDefinition generatedLootDefinition)
    {
        List<Vector3>  lootPositions = _lootSpawnPoints.Select(x => x.position).ToList();
        List<Quaternion> lootRotations = _lootSpawnPoints.Select(x => x.rotation).ToList();
        
        
        List<Vector3>  cookiesPositions = _cookiesSpawnPoints.Select(x => x.position).ToList();
        
        int lootCount = Random.Range(_minCookies, _lootSpawnPoints.Length);
        for (int i = 0; i < lootCount; i++)
        {
            if (lootPositions.Count == 0) break;
            int lootPosition = Random.Range(0, lootPositions.Count);
            int lootGenerated = Random.Range(0, generatedLootDefinition.GeneratedLoots.Length);
            var newLoot = Instantiate(generatedLootDefinition.GeneratedLoots[lootGenerated], 
                lootPositions[lootPosition], lootRotations[lootPosition]);
            newLoot.transform.parent = transform;
            lootPositions.RemoveAt(lootPosition);
            lootRotations.RemoveAt(lootPosition);
        }
        lootCount = Random.Range(_minCookies, _cookiesSpawnPoints.Length);
        for (int i = 0; i < lootCount; i++)
        {
            if (cookiesPositions.Count == 0) break;
            int lootPosition = Random.Range(0, cookiesPositions.Count);
            int lootGenerated = Random.Range(0, generatedLootDefinition.Cookies.Length);
            var newLoot = Instantiate(generatedLootDefinition.Cookies[lootGenerated], 
                cookiesPositions[lootPosition], Quaternion.identity);
            newLoot.transform.parent = transform;
            cookiesPositions.RemoveAt(lootPosition);
        }
    }

    public void GenerateEnemies()
    {
        foreach (var ai in _ais)
        {
            ai.gameObject.SetActive(true);
            ai.Init();
        }
    }
}
