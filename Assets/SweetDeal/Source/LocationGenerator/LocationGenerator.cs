using System.Collections.Generic;
using SweetDeal.Source.LocationGenerator.Configs;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator
{
    public class LocationGenerator : MonoBehaviour
    {
        void GenerateHub(LocationScriptableObject locationInfo, Door mainDoor, List<Door> doors)
        {
            int hubIndex = Random.Range(0, locationInfo.Hub.Length);
            var hub = Instantiate(locationInfo.Hub[hubIndex]);
            hub.transform.forward = mainDoor.transform.forward;
            hub.transform.position = mainDoor.transform.position;
            doors.AddRange(hub.Exites);
        }

        bool CanPlace(Vector3 point, float radius)
        {
            return !Physics.CheckSphere(point, radius);
        }
        
        void GenerateRoom(LocationScriptableObject locationInfo, Door door, int count, List<Door> freeDoors, List<Room> rooms)
        {
            
            int roomIndex = 0;
            Room room = null;
            if (count > freeDoors.Count)
            {
                roomIndex = Random.Range(0, locationInfo.OneAndMoreDoors.Length);


                var position = door.Position + door.transform.forward * locationInfo.OneAndMoreDoors[roomIndex].Radius;
                if (!CanPlace(position, locationInfo.OneAndMoreDoors[roomIndex].Radius))
                {
                    return;
                }
                
                room = Instantiate(locationInfo.OneAndMoreDoors[roomIndex]);
            }
            else
            {
                
                var position = door.Position + door.transform.forward * locationInfo.OneAndMoreDoors[roomIndex].Radius;
                if (!CanPlace(position, locationInfo.OneAndMoreDoors[roomIndex].Radius))
                {
                    return;
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
        }
        
        public void Generate(LocationScriptableObject locationInfo, Door mainDoor)
        {
            List<Door> freeDoors = new List<Door>();
            List<Room> freeRooms = new List<Room>();
            int count = locationInfo.Count;
            
            GenerateHub(locationInfo, mainDoor, freeDoors);

            while (count > 0)
            {
                int nextDoor = Random.Range(0, freeDoors.Count);
                Door door =  freeDoors[nextDoor];
                freeDoors.RemoveAt(nextDoor);
            
                GenerateRoom(locationInfo, door, count, freeDoors, freeRooms);

                count--;
            }

            foreach (var room in freeRooms)
            {
                room.GenerateLoot();
            }
            
            freeDoors.Clear();
            freeRooms.Clear();

            
        }
    }
}