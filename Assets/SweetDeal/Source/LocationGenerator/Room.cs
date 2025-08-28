using System.Collections.Generic;
using System.Linq;
using SweetDeal.Source.AI;
using SweetDeal.Source.LocationGenerator;
using SweetDeal.Source.LocationGenerator.Configs;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Door[] _exites;
    [SerializeField] private Door _entry;
    [SerializeField] private BehaviourConstruct[] _ais;

    [SerializeField] private Transform[] _lootSpawnPoints;
    [SerializeField] private Vector2Int _lootCountsRange;
    
    public IEnumerable<Door> Exites => _exites;
    public Door Entry => _entry;
    
    
    public void GenerateLoot(GeneratedLootDefinition generatedLootDefinition)
    {
        List<Vector3>  lootPositions = _lootSpawnPoints.Select(x => x.position).ToList();
        List<Quaternion> lootRotations = _lootSpawnPoints.Select(x => x.rotation).ToList();
        
        int lootCount = Random.Range(_lootCountsRange.x, _lootCountsRange.y + 1);
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
    }

    public void GenerateEnemies()
    {
        foreach (var ai in _ais)
        {
            ai.Init();
        }
    }

}
