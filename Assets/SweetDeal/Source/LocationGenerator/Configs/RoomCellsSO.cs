using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomCellsEntry
{
    public string RoomId;
    public Vector2Int Size;
    public Vector2Int DoorOffset;
    public Vector3[] Cells;
}

[CreateAssetMenu(menuName = "Location/RoomCellsSO")]
public class RoomCellsSO : ScriptableObject
{
    public List<RoomCellsEntry> Rooms = new List<RoomCellsEntry>();
    public int maxLenght;
    public int maxWidth;

    public void SetCells(string roomId, Vector3[] cells, Vector2Int size, Vector2Int doorOffset)
    {
        var entry = Rooms.Find(e => e.RoomId == roomId);
        if (entry != null)
        {
            entry.Cells = cells;
            entry.Size = size;
            entry.DoorOffset = doorOffset;
            if (size.x > maxWidth)
            {
                maxWidth = size.x;
            }

            if (size.y > maxLenght)
            {
                maxLenght = size.y;
            }
        }
        else
            Rooms.Add(new RoomCellsEntry { RoomId = roomId, Cells = cells, Size = size });
    }

    public Vector3[] GetCells(string roomId)
    {
        var entry = Rooms.Find(e => e.RoomId == roomId);
        return entry != null ? entry.Cells : null;
    }
}