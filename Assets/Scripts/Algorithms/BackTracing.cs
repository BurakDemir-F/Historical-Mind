using System.Collections.Generic;
using System.Linq;
using Maze;
using UnityEngine;

namespace Algorithms
{
    public class BackTracing
    {
        private List<Cell> _cells;
        private Vector2Int _size;

        private List<Cell> _neighborCellsTemp;
        private MazeBase _maze;
        public BackTracing(Vector2Int size)
        {
            _cells = new List<Cell>();
            _size = size;
            GenerateCells();
        }

        public BackTracing(int sizeX, int sizeY)
        {
            _cells = new List<Cell>();
            _size = new Vector2Int(sizeX, sizeY);
        }
        
        public BackTracing(Vector2Int size, int backTraceCount)
        {
            _cells = new List<Cell>();
            _size = size;
        }

        public BackTracing(int sizeX, int sizeY, int backTraceCount)
        {
            _cells = new List<Cell>();
            _size = new Vector2Int(sizeX, sizeY);
        }

        public List<Cell> GetCells()
        {
            return _cells;
        }

        public void GenerateCells()
        {
            GenerateCellsInternal(_size.x,_size.y);
            PerformBackTracing();
            SetNeighborCells();
        }

        public List<Cell> GetFullTracedCells()
        {
            GenerateCellsInternal(_size.x,_size.y); 
            SetVisitedAll();
            SetNeighborCells();
            return _cells;
        }

        private void SetVisitedAll()
        {
            foreach (var cell in _cells)
            {
                cell.SetVisited();
            }
        }

        private void GenerateCellsInternal(int sizeX, int sizeY)
        {
            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    _cells.Add(new Cell(j,i,false, 
                        new NeighborData(new List<bool>{false,false,false,false})));
                }
            }
        }

        private void PerformBackTracing()
        {
            var size = _size.x * _size.y;
            var pathStack = new Stack<Cell>();
            var currentCell = _cells[0];
            _neighborCellsTemp = new List<Cell>();

            for (var i = 0; i < size; i++)
            {
                _neighborCellsTemp.Clear();
                _neighborCellsTemp = GetNeighborCells(_cells,currentCell);

                if (_neighborCellsTemp.Count == 0)
                {
                    if (pathStack.Count == 0)
                    {
                        break;
                    }
                    currentCell.SetVisited();
                    currentCell = pathStack.Pop();
                }
                else
                {
                    currentCell.SetVisited();
                    pathStack.Push(currentCell);
                    var newCell = _neighborCellsTemp[Random.Range(0, _neighborCellsTemp.Count)];
                    currentCell = newCell;
                }
            }
        }

        private List<Cell> GetNeighborCells(IReadOnlyList<Cell> cells, Cell currentCell) // 0,1 2,0
        {
            var neighborCells = new List<Cell>();

            var currentPosX = currentCell.Position.x; // 0
            var currentPosY = currentCell.Position.y; // 1
            var lastCell = cells[^1];
            var firstCell = cells[0];
            var cellPosition = GetCellPosition(currentCell);

            //check up neighbor
            if (firstCell.Position.y < currentPosY && !cells[cellPosition - _size.x].IsVisited)
            {
                neighborCells.Add(cells[cellPosition - _size.x]);
            }

            //check down neighbor
            if (lastCell.Position.y > currentPosY && !cells[cellPosition + _size.x].IsVisited)
            {
                neighborCells.Add(cells[cellPosition + _size.x]);
            }

            //check left neighbor
            if(firstCell.Position.x < currentPosX && !cells[cellPosition - 1].IsVisited)
            {
                neighborCells.Add(cells[cellPosition - 1]);
            }

            //check right neighbor
            if(lastCell.Position.x > currentPosX && !cells[cellPosition + 1].IsVisited)
            {
                neighborCells.Add(cells[cellPosition + 1]);
            }

            return neighborCells;
        }

        private void SetNeighborCells()
        {
            foreach (var cell in _cells)
            {
                SetNeighbors(_cells,cell);
            }
        }

        private void SetNeighbors(IReadOnlyList<Cell> cells, Cell currentCell)
        {

            var currentPosX = currentCell.Position.x; 
            var currentPosY = currentCell.Position.y; 
            var lastCell = cells[^1];
            var firstCell = cells[0];
            var cellPosition = GetCellPosition(currentCell);

            //check up neighbor
            if (firstCell.Position.y < currentPosY && cells[cellPosition - _size.x].IsVisited)
            {
                currentCell.SetSideNeighbor(0);
                currentCell.AddNeighborCell(cells[cellPosition - _size.x]);
            }

            //check down neighbor
            if (lastCell.Position.y > currentPosY && cells[cellPosition + _size.x].IsVisited)
            {
                currentCell.SetSideNeighbor(1);
                currentCell.AddNeighborCell(cells[cellPosition + _size.x]);
            }

            //check left neighbor
            if(firstCell.Position.x < currentPosX && cells[cellPosition - 1].IsVisited)
            {
                currentCell.SetSideNeighbor(3);
                currentCell.AddNeighborCell(cells[cellPosition - 1]);
            }

            //check right neighbor
            if(lastCell.Position.x > currentPosX && cells[cellPosition + 1].IsVisited)
            {
                currentCell.SetSideNeighbor(2);
                currentCell.AddNeighborCell(cells[cellPosition + 1]);
            }

        }

        private int GetCellPosition(Cell cell)
        {
            var cellPosY = cell.Position.y;
            var cellPosX = cell.Position.x;
            
            return cellPosY * _size.x + cellPosX;
        }

        #region Helpers



        #endregion
    }
}
