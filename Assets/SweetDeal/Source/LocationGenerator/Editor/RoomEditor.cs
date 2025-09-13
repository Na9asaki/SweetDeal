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

            MeshRenderer col = room.Renderer;

            Vector3 size, center;
            size = col.bounds.size;
            center = col.bounds.center;

            int cellsX = Mathf.CeilToInt(size.x / SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE);
            int cellsZ = Mathf.CeilToInt(size.z / SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE);

            List<Vector3> centers = new List<Vector3>();
            for (int i = 0; i < cellsX; i++)
            {
                for (int j = 0; j < cellsZ; j++)
                {
                    Vector3 localPoint = new Vector3(
                        i * SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE + SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE / 2f - size.x / 2,
                        center.y,
                        j * SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE + SweetDeal.Source.LocationGenerator.Grid.ROOM_CELL_SIZE / 2f
                    );
                    Debug.Log(localPoint);
                    
                    centers.Add(localPoint);
                }
            }

            room.RoomCells.SetCells(room.name, centers.ToArray(), new Vector2Int(cellsX, cellsZ));
            EditorUtility.SetDirty(room.RoomCells);
            AssetDatabase.SaveAssets();

            Debug.Log($"Baked {centers.Count} cells relative to entry for room {room.name}");
        }
    }
}
#endif
