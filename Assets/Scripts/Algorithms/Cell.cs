using System.Collections.Generic;
using System.Linq;
using Storing;
using UnityEngine;
using Utilities;

namespace Algorithms
{
    [System.Serializable]
    public class Cell : IBackTrace
    {
        public Vector2Int Position { get; private set; }
        public bool IsVisited { get; private set; }
        public bool IsDangerous { get; private set; }
        private NeighborData _neighborData;
        public Cell(Vector2Int position, bool isVisited,NeighborData neighborData)
        {
            Position = position;
            IsVisited = isVisited;
            _neighborData = neighborData;
        }
        public Cell(int positionX, int positionY, bool isVisited, NeighborData neighborData)
        {
            Position = new Vector2Int(positionX, positionY);
            IsVisited = isVisited;
            _neighborData = neighborData;
        }

        public void AddNeighborCell(Cell neighbor)
        {
            _neighborData.AddNeighborCell(neighbor);
        }

        public List<Cell> GetNeighborCells()
        {
            return _neighborData.GetNeighborCells();
        }
        
        public void SetSideNeighbor(int side) => _neighborData.SetNeighborStatus(side);
        public void SetVisited() => IsVisited = true;
        public void SetDangerous() => IsDangerous = true;

        public List<bool> GetNeighborStatuses() => _neighborData.GetNeighborStatuses();

        public int PositionToIndex(int width)
        {
            return GetIndex(Position.x, Position.y, width);
        }
        
        public static int GetIndex(int x, int y, int width)
        {
            return y * width + x;
        }
        
        public bool HasUpNeighbor() => GetNeighborStatuses()[0];
        public bool HasDownNeighbor() => GetNeighborStatuses()[1];
        public bool HasRightNeighbor() => GetNeighborStatuses()[2];
        public bool HasLeftNeighbor() => GetNeighborStatuses()[3];

        public SerializableCell ToSerializable()
        {
            return new SerializableCell(_neighborData.ToSerializable(),Position.ToSerializable(),IsVisited,IsDangerous);
        }
    }

    [System.Serializable]
    public class SerializableCell
    {
        public Vector2IntSerializable Position { get; set; }
        public bool IsVisited { get; set; }
        public bool IsDangerous { get; set; }
        private SerializableNeighborData _neighborData;

        public SerializableCell(SerializableNeighborData neighborData, Vector2IntSerializable position, bool isVisited, bool isDangerous)
        {
            _neighborData = neighborData;
            Position = position;
            IsVisited = isVisited;
            IsDangerous = isDangerous;
        }
    }
    [System.Serializable]
    public class SerializableNeighborData
    {
        public List<SerializableCell> cells;
        public List<bool> neighborStatuses;

        public SerializableNeighborData(NeighborData neighborData, List<SerializableCell> serializableCells)
        {
            var neighborDataList = neighborData.GetNeighborCells();
            cells = /*(from nd in neighborDataList select nd.ToSerializable()).ToList()*/ serializableCells;
            neighborStatuses = neighborData.GetNeighborStatuses();
        }
    }

    public static class CellExtensions
    {
        public static bool HasUpCornerNeighbor(this Cell cell)
        {
            var hasUpNeighbor = cell.HasUpNeighbor();
            var hasLeftNeighbor = cell.HasLeftNeighbor();
            var hasRightNeighbor = cell.HasRightNeighbor();

            if (!hasUpNeighbor && !hasLeftNeighbor && !hasRightNeighbor) return false;

            var neighbors = cell.GetNeighborCells();

            if (hasUpNeighbor && neighbors[0].HasLeftNeighbor() || neighbors[0].HasRightNeighbor()) return true;
            if (hasLeftNeighbor && neighbors[3].HasUpNeighbor()) return true;
            if (hasRightNeighbor && neighbors[2].HasUpNeighbor()) return true;
            
            return false;
        }
        
        public static bool HasDownCornerNeighbor(this Cell cell)
        {
            var hasDownNeighbor = cell.HasDownNeighbor();
            var hasLeftNeighbor = cell.HasLeftNeighbor();
            var hasRightNeighbor = cell.HasRightNeighbor();

            if (!hasDownNeighbor && !hasLeftNeighbor && !hasRightNeighbor) return false;

            var neighbors = cell.GetNeighborCells();

            if (hasDownNeighbor && neighbors[1].HasLeftNeighbor() || neighbors[1].HasRightNeighbor()) return true;
            if (hasLeftNeighbor && neighbors[3].HasDownNeighbor()) return true;
            if (hasRightNeighbor && neighbors[2].HasDownNeighbor()) return true;
            
            return false;
        }

        
        // bu fonksiyon olmadı...
        public static bool TryGetCornerNeighbors(this Cell cell, out List<Cell> outputCells)
        {
            var hasUpNeighbor = cell.HasUpNeighbor();
            var hasLeftNeighbor = cell.HasLeftNeighbor();
            var hasRightNeighbor = cell.HasRightNeighbor();
            var hasDownNeighbor = cell.HasDownNeighbor();

            if (!hasUpNeighbor && !hasLeftNeighbor && !hasRightNeighbor && !hasDownNeighbor)
            {
                outputCells = null;
                return false;
            }

            var squareNeighbors = cell.GetNeighborCells();
            outputCells = new List<Cell>();
            
            if(hasUpNeighbor && squareNeighbors[0].HasLeftNeighbor()) 
                outputCells.Add(squareNeighbors[0].GetNeighborCells()[3]);
            
            if(hasUpNeighbor && squareNeighbors[0].HasRightNeighbor()) 
                outputCells.Add(squareNeighbors[0].GetNeighborCells()[2]);
            
            if(hasDownNeighbor && squareNeighbors[1].HasLeftNeighbor()) 
                outputCells.Add(squareNeighbors[1].GetNeighborCells()[3]);
            
            if(hasDownNeighbor && squareNeighbors[1].HasRightNeighbor()) 
                outputCells.Add(squareNeighbors[1].GetNeighborCells()[2]);

            if (outputCells.Count > 0) return true;
            else
            {
                outputCells = null;
                return false;
            }
        }
    }
}