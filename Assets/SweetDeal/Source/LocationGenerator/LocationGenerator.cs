using System;
using System.Collections;
using System.Collections.Generic;
using SweetDeal.Source.LocationGenerator.Configs;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SweetDeal.Source.LocationGenerator
{
    public class LocationGenerator : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface  _navMeshSurface;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private GeneratedLootDefinition _generatedLootDefinitions;
        [SerializeField] private Grid _grid;
        [SerializeField] private float _roomRadius;
        [SerializeField] private float _doorTreshold;
        
        private List<Room> _rooms =  new List<Room>();
        public event Action RestartComplete;
        
        void GenerateHub(LocationScriptableObject locationInfo, Door mainDoor, List<Door> doors)
        {
            int hubIndex = Random.Range(0, locationInfo.Hub.Length);
            var hub = Instantiate(locationInfo.Hub[hubIndex]);
            _grid.Fill(mainDoor.Position + mainDoor.transform.forward * _grid.CellSize / 2);
            hub.transform.forward = mainDoor.transform.forward;
            hub.transform.position = mainDoor.transform.position;
            doors.AddRange(hub.Exites);
            _rooms.Add(hub);
        }
        
        bool GenerateRoom(LocationScriptableObject locationInfo, Door door, int count, List<Door> freeDoors, List<Room> rooms)
        {
            
            int roomIndex = 0;
            Room room = null;
            if (count > freeDoors.Count)
            {
                roomIndex = Random.Range(0, locationInfo.OneAndMoreDoors.Length);
                var pos = door.Position + door.transform.forward * _grid.CellSize / 2;
                if (!_grid.IsFree(pos))
                {
                    return false;
                }
                else
                {
                    _grid.Fill(pos);
                }
                
                room = Instantiate(locationInfo.OneAndMoreDoors[roomIndex]);
            }
            else
            {
                var pos = door.Position + door.transform.forward * _grid.CellSize / 2;
                if (!_grid.IsFree(pos))
                {
                    Destroy(door.gameObject);
                    return false;
                }
                else
                {
                    _grid.Fill(pos);
                }
                
                roomIndex = Random.Range(0, locationInfo.NoDoors.Length);
                room = Instantiate(locationInfo.NoDoors[roomIndex]);
            }
                
            room.transform.forward = door.transform.forward;
            room.transform.position = door.Position;
                
            freeDoors.AddRange(room.Exites);
            rooms.Add(room);
                
            Destroy(door.gameObject);
            room.Entry.Activate();
            return true;
        }

        void GenerateExit(LocationScriptableObject locationInfo, Door door)
        {
            var exit = Instantiate(locationInfo.Exit);
            exit.transform.forward = door.transform.forward;
            exit.transform.position = door.transform.position;
            exit.Entry.Activate();
            Destroy(door.gameObject);
            _rooms.Add(exit);
        }

        void GenerateNavigation()
        {
            _navMeshSurface.BuildNavMesh();
        }
        
        public void Generate(LocationScriptableObject locationInfo, Door mainDoor)
        {
            _grid.Init();
            
            List<Door> freeDoors = new List<Door>();
            int count = locationInfo.Count;
            
            GenerateHub(locationInfo, mainDoor, freeDoors);

            while (count > 0)
            {
                int nextDoor = Random.Range(0, freeDoors.Count);
                Door door =  freeDoors[nextDoor];
                freeDoors.RemoveAt(nextDoor);
                if (GenerateRoom(locationInfo, door, count, freeDoors, _rooms))
                {
                    Debug.Log("Room generated");
                    count--;
                }

                if (freeDoors.Count == 1 || count == 1)
                {
                    nextDoor = Random.Range(0, freeDoors.Count);
                    door =  freeDoors[nextDoor];
                    GenerateExit(locationInfo, door);
                    break;
                }
            }
            
            
            freeDoors.Clear();

            StartCoroutine(Rebuild());
        }

        public void Restart()
        {
            if (_rooms == null) return;
            foreach (var room in _rooms)
            {
                Destroy(room.gameObject);
            }
            _rooms.Clear();
            _navMeshSurface.RemoveData();
        }

        private IEnumerator Rebuild()
        {
            yield return null;
            GenerateNavigation();
            foreach (var room in _rooms)
            {
                room.GenerateLoot(_generatedLootDefinitions);
                room.GenerateEnemies();
            }
            RestartComplete?.Invoke();
        }
    }
}