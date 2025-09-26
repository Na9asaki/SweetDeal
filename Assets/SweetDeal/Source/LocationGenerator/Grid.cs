using System;
using System.Collections.Generic;
using System.Linq;
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
        public const int ROOM_CELL_SIZE = 4;
        
        private static bool[,] _grid;

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
        
        public List<Room> CanPlace(Vector3 doorWorldPos, Vector3 doorDirectionWorld, List<Room> prefabs)
        {
            var result = new List<Room>();
            if (_grid == null || prefabs == null || prefabs.Count == 0) return result;

            // origin — клетка у двери (или прямо перед дверью)
            Vector2Int origin = GetGridPosition(doorWorldPos + doorDirectionWorld.normalized * (CellSize * 0.5f));
            Vector2Int forwardCell = GetGridPosition(doorWorldPos + doorDirectionWorld.normalized * CellSize);

            Vector2Int verticalStep = forwardCell - origin; // (dr, dc) — гарантированно маленькие целые значения

            if (verticalStep == Vector2Int.zero)
            {
                Debug.LogWarning($"CanPlace: verticalStep == Vector2Int.zero. doorDir={doorDirectionWorld} origin={origin} forward={forwardCell}");
                return result;
            }

            // rotate 90° to get horizontal step (right)
            Vector2Int horizontalStep = new Vector2Int(-verticalStep.y, verticalStep.x);

            // caps from prefabs (быстрый safe cap)
            int maxDepth = prefabs.Max(pf => pf.RoomCells.maxLenght);
            int maxWidth = prefabs.Max(pf => pf.RoomCells.maxWidth);

            // 1) посчитать максимальную глубину (depth) - подряд свободных клеток по forward
            int depth = 0;
            for (int d = 1; d <= maxDepth; d++)
            {
                Vector2Int check = origin + verticalStep * d;
                if (!IsInsideGrid(check) || _grid[check.x, check.y]) break;
                depth++;
            }

            if (depth == 0) return result;

            // 2) для каждой строки глубины посчитать, сколько слева/справа свободно, взять минимумы
            int minLeft = int.MaxValue;
            int minRight = int.MaxValue;

            for (int d = 1; d <= depth; d++)
            {
                Vector2Int rowOrigin = origin + verticalStep * d;

                int leftCount = 0;
                for (int w = 1; w <= maxWidth; w++)
                {
                    Vector2Int p = rowOrigin - horizontalStep * w; // влево
                    if (!IsInsideGrid(p) || _grid[p.x, p.y]) break;
                    leftCount++;
                }

                int rightCount = 0;
                for (int w = 1; w <= maxWidth; w++)
                {
                    Vector2Int p = rowOrigin + horizontalStep * w; // вправо
                    if (!IsInsideGrid(p) || _grid[p.x, p.y]) break;
                    rightCount++;
                }

                minLeft = Math.Min(minLeft, leftCount);
                minRight = Math.Min(minRight, rightCount);
            }

            int availableWidth = 1 + minLeft + minRight; // включая сам столбец двери

            // 3) фильтруем префабы: быстрый фильтр по габаритам, затем по смещению двери
            // ПРИМЕЧАНИЕ: здесь я предполагаю, что у тебя в RoomCellsSO есть поля Size (width,height) и DoorOffset (offsetFromLeft)
            foreach (var prefab in prefabs)
            {
                var meta = prefab.RoomCells.Rooms.First(x => x.RoomId.Equals(prefab.name)); // реализуй GetEntry, чтобы вернуть Size и DoorOffset
                if (meta == null) continue;

                Vector2Int roomSize = meta.Size;     // roomSize.x = width, roomSize.y = length (в клетках)
                int doorOffsetFromLeft = meta.DoorOffset.x; // число клеток слева от двери в префабе

                int requiredLeft = doorOffsetFromLeft;
                int requiredRight = roomSize.x - doorOffsetFromLeft - 1;

                if (roomSize.y <= depth && requiredLeft <= minLeft && requiredRight <= minRight)
                {
                    result.Add(prefab);
                }
            }

            Debug.Log($"CanPlace: depth={depth}, left={minLeft}, right={minRight}, candidates={result.Count}");
            return result;
        }

        // помощьники:
        private bool IsInsideGrid(Vector2Int p)
        {
            if (_grid == null) return false;
            return p.x >= 0 && p.x < _grid.GetLength(0) && p.y >= 0 && p.y < _grid.GetLength(1);
        }

        /*
        public List<Room> CanPlace(Vector3 door, Vector3 doorDirection, List<Room> prefabs)
        {
            Debug.Log("Scan free space");
            List<Room> rooms = new List<Room>();
            var startPosition = GetGridPosition(door + doorDirection * CellSize / 2);
            var endPosition = GetGridPosition(door + doorDirection * CellSize * prefabs[0].RoomCells.maxLenght);
            var verticalStep = new Vector2Int((int)doorDirection.z, (int)doorDirection.x);
            var horizontalStep = new Vector2Int((int)doorDirection.x, (int)doorDirection.z);
            int lOffset = int.MaxValue, rOffset = int.MaxValue;
            int length = 0;
            int iterations = prefabs[0].RoomCells.maxLenght;
            //Debug.Log($"{door} {door + doorDirection * CellSize * prefabs[0].RoomCells.maxLenght} {startPosition} to {endPosition} with {verticalStep}:{horizontalStep}");
            //Debug.Log($"{prefabs[0].RoomCells.maxLenght} : {prefabs[0].RoomCells.maxWidth}");

            while (startPosition != endPosition && isFree(startPosition) && iterations > 0)
            {
                iterations--;
                int loclOffset = 0, locrOffset = 0;
                while (isFree(startPosition + horizontalStep * locrOffset) && locrOffset < prefabs[0].RoomCells.maxWidth)
                {
                    locrOffset++;
                    if (locrOffset == 10) break;
                    if (locrOffset > rOffset) break;
                }

                while (isFree(startPosition - horizontalStep * loclOffset) && loclOffset < prefabs[0].RoomCells.maxLenght)
                {
                    loclOffset++;
                    if (loclOffset == 10) break;
                    if (loclOffset > lOffset) break;
                }


                if (loclOffset < lOffset)
                {
                    lOffset = loclOffset;
                }

                if (locrOffset < rOffset)
                {
                    rOffset = locrOffset;
                }

                length++;
                startPosition += verticalStep;
            }
            Debug.Log("Calculating result");

            var result =  prefabs[0].RoomCells.Rooms.Where(x => x.Size.y <= length && x.DoorOffset.x < lOffset &&
                                          x.DoorOffset.y <= rOffset);

            foreach (var roomId in result)
            {
                var rm = prefabs.Find(x => x.name.Equals(roomId.RoomId));
                if (rm != null)
                    rooms.Add(rm);
            }
            
            Debug.Log("Found " + rooms.Count + " rooms");
            return rooms;

            bool isFree(Vector2Int position)
            {
                if (position.x >= _grid.GetLength(0) || position.x < 0 
                    || position.x >= _grid.GetLength(1))
                {
                    Debug.Log("Index out of range");
                    return false;
                }
                if (position.y >= _grid.GetLength(0) || position.y < 0 
                                                     || position.y >= _grid.GetLength(1))
                {
                    Debug.Log("Index out of range");
                    return false;
                }
                return !_grid[position.x, position.y];
            }
        }
        */

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
        private void OnDrawGizmos()
        {
            if (_grid == null || StartCorner == null || EndCorner == null)
                return;

            Gizmos.matrix = Matrix4x4.identity;

            int height = _grid.GetLength(0);
            int width = _grid.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // центр клетки в мировых координатах
                    Vector3 cellCenter = StartCorner.position + new Vector3(
                        (x + 0.5f) * CellSize,
                        0f,
                        (y + 0.5f) * CellSize
                    );

                    // цвет по занятости
                    Gizmos.color = _grid[y, x] ? Color.red : Color.green;

                    // рисуем квадратик (плоский куб)
                    Gizmos.DrawCube(cellCenter, new Vector3(CellSize, 0.1f, CellSize));
                }
            }
        }

    }
}