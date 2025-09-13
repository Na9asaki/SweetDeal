using System;
using System.Collections.Generic;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator
{
    public class Grid : MonoBehaviour
    {
        [field: SerializeField] public Transform StartCorner {get; private set;}
        [field: SerializeField] public Transform EndCorner {get; private set;}
        [field: SerializeField] public float CellSize { get; private set; } = 40;
        
        private static Vector3 endCorner;
        private static Vector3 startCorner;
        public static float cellSize;
        public const int ROOM_CELL_SIZE = 5;
        
        private static bool[,] _grid;

        private void OnDrawGizmos()
        {
            if (_grid == null) return;

            // Пройдем по всем ячейкам
            int rows = _grid.GetLength(0);
            int cols = _grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Вычисляем позицию центра ячейки
                    Vector3 cellCenter = startCorner + new Vector3(
                        j * CellSize + CellSize / 2f,
                        0f,
                        i * CellSize + CellSize / 2f
                    );

                    // Цвет по значению ячейки
                    Gizmos.color = _grid[i, j] ? Color.red : Color.green;

                    // Рисуем куб размером cellSize
                    Gizmos.DrawWireCube(cellCenter, new Vector3(CellSize, 0.1f, CellSize));
                }
            }
        }

        public static Vector2Int WorldToGrid(Vector3 position)
        {
            var delta = position - startCorner;
            int x = Mathf.FloorToInt(delta.x / cellSize);
            int y = Mathf.FloorToInt(delta.z / cellSize);
            
            x = Mathf.Clamp(x, 0, _grid.GetLength(1) - 1);
            y = Mathf.Clamp(y, 0, _grid.GetLength(0) - 1);
            
            return new Vector2Int(y, x);
        }

        public void Init()
        {
            int width = Mathf.FloorToInt((EndCorner.position.x - StartCorner.position.x) / CellSize);
            int height = Mathf.FloorToInt((EndCorner.position.z - StartCorner.position.z) / CellSize);
            _grid = new bool[height, width];
            cellSize = CellSize;
            startCorner = StartCorner.position;
            endCorner = EndCorner.position;
        }

        private Vector2Int GetGridPosition(Vector3 position)
        {
            var delta = position - StartCorner.position;
            int x = Mathf.FloorToInt(delta.x / CellSize);
            int y = Mathf.FloorToInt(delta.z / CellSize);
            x = Mathf.Clamp(x, 0, _grid.GetLength(1) - 1);
            y = Mathf.Clamp(y, 0, _grid.GetLength(0) - 1);
            return new Vector2Int(y, x);
        }

        public bool IsFree(Vector3[] position, Vector3 door, Quaternion doorRotation)
        {
            foreach (var pos in position)
            {
                var gridPosition = GetGridPosition(doorRotation * pos + door);
                if (_grid[gridPosition.x, gridPosition.y])
                {
                    return false;
                }
            }
            return true;
        }

        public void Fill(Vector3[] position, Vector3 door, Quaternion doorRotation)
        {
            foreach (var pos in position)
            {
                var gridPosition = GetGridPosition(doorRotation * pos + door);
                _grid[gridPosition.x, gridPosition.y] = true;
            }
        }

        private void OnDestroy()
        {
            _grid = null;
        }
    }
}