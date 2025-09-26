#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Room room = (Room)target;

        if (GUILayout.Button("Bake Cell Centers Relative To Entry"))
        {
            if (room.RoomCells == null)
            {
                Debug.LogError("Assign RoomCellsSO asset first!");
                return;
            }

            BoxCollider col = room.Floor;
            MeshRenderer[] floors = col.GetComponentsInChildren<MeshRenderer>();
        
            Vector3 size, center;
            List<Vector3> cells = new List<Vector3>();
            int cellxCount = 0;
            int cellzCount = 0;
            
            size = col.size;

            int cellsX = Mathf.CeilToInt(size.x / SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE);
            int cellsZ = Mathf.CeilToInt(size.z / SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE);
                
            cellxCount += cellsX;
            cellzCount += cellsZ;
            Debug.Log(floors.Length);

            List<Vector3> centers = new List<Vector3>();
            foreach (MeshRenderer floor in floors)
            {
                centers.Add(floor.bounds.center);
            }
            /*
            for (int i = 0; i < cellsX; i++)
            {
                for (int j = 0; j < cellsZ; j++)
                {
                    Vector3 localPoint = new Vector3(
                        i * SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE +
                        SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE / 2f - size.x / 2,
                        0,
                        j * SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE +
                        SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE / 2f
                    );
                    foreach (var floor in floors)
                    {
                        if (Vector3.SqrMagnitude(floor.bounds.center - localPoint) <= .01f)
                        {
                            //Debug.Log(floor.bounds.center);
                            Debug.Log(true);
                            centers.Add(localPoint);
                            break;
                        }
                    }
                    Debug.Log("--------------------");
                }
            }
            */
            cells.AddRange(centers);
            room.RoomCells.SetCells(room.name, cells.ToArray(), new Vector2Int(cellxCount, cellzCount), room.DoorOffset);
            EditorUtility.SetDirty(room.RoomCells);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
