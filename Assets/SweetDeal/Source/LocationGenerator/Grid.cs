using System;
using UnityEngine;

namespace SweetDeal.Source.LocationGenerator
{
    public class Grid : MonoBehaviour
    {
        [field: SerializeField] public Vector3 StartCorner {get; private set;}
        [field: SerializeField] public Vector3 EndCorner {get; private set;}
        [feidl: SerializeField] public float CellSize { get; private set; } = 40;
        
        private bool[,] _grid;

        public void Init()
        {
            int width = Mathf.RoundToInt((EndCorner.x - StartCorner.x) / CellSize);
            int height = Mathf.RoundToInt((EndCorner.z - StartCorner.z) / CellSize);
            _grid = new bool[height, width];
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
    }
}