using System;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator
{
    public class Grid : MonoBehaviour
    {
        [field: SerializeField] public Vector3 StartCorner {get; private set;}
        [field: SerializeField] public Vector3 EndCorner {get; private set;}
        [field: SerializeField] public float CellSize { get; private set; } = 40;
        
        private static Vector3 endCorner;
        private static Vector3 startCorner;
        public static float cellSize;
        
        private static bool[,] _grid;

        public static Vector2Int WorldToGrid(Vector3 position)
        {
            var delta = position - startCorner;
            int x = Mathf.RoundToInt(delta.x / cellSize);
            int y = Mathf.RoundToInt(delta.z / cellSize);
            return new Vector2Int(y, x);
        }

        public void Init()
        {
            Debug.Log("Init");
            int width = Mathf.RoundToInt((EndCorner.x - StartCorner.x) / CellSize);
            int height = Mathf.RoundToInt((EndCorner.z - StartCorner.z) / CellSize);
            _grid = new bool[height, width];
            cellSize = CellSize;
            startCorner = StartCorner;
            endCorner = EndCorner;
        }

        private Vector2Int GetGridPosition(Vector3 position)
        {
            var delta = position - StartCorner;
            int x = Mathf.RoundToInt(delta.x / CellSize);
            int y = Mathf.RoundToInt(delta.z / CellSize);
            return new Vector2Int(y, x);
        }

        public bool IsFree(Vector3 position)
        {
            var gridPosition = GetGridPosition(position);
            if (_grid[gridPosition.x, gridPosition.y]) return false;
            return true;
        }

        public void Fill(Vector3 position)
        {
            var gridPosition = GetGridPosition(position);
            _grid[gridPosition.x, gridPosition.y] = true;
        }

        private void OnDestroy()
        {
            _grid = null;
        }
    }
}