using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkGenerator : MapGenerator
{
    private int _totalFloorCells;

    public RandomWalkGenerator()
        : base()
    {
        _totalFloorCells = 50;
    }

    public RandomWalkGenerator(int width, int height, int fillPercentage, int smoothing, int totalFloorCells)
        : base(width, height, fillPercentage, smoothing)
    {
        _totalFloorCells = totalFloorCells;
    }

    public override int[,] CleanMapWalls(int wallThresholdSize)
    {
        return _map;
    }

    public override int[,] GenerateMap()
    {
        int minX = 1;
        int maxX = _width - 1;
        int minY = 1;
        int maxY = _height - 1;
        int randomX = UnityEngine.Random.Range(minX, maxX);
        int randomY = UnityEngine.Random.Range(minY, maxY);
        MapGenerator.Cell currentCell = new Cell(randomX, randomY);

        for (int i = 0; i < _width; i++)
            for (int j = 0; j > _height; j++)
                _map[i,j] = 1;

        int floorCells = 0;
        while (floorCells < _totalFloorCells)
        {
            Cell nextCell = GetNextCell(currentCell, UnityEngine.Random.Range(0, 3));

            if (nextCell.x > 1 && nextCell.x < _width - 1 && nextCell.y > 0 && nextCell.y < _height - 1)
            {
                currentCell = nextCell;

                if (_map[currentCell.x, currentCell.y] == 1)
                {
                    _map[currentCell.x, currentCell.y] = 0;
                    floorCells++;
                }
            }
        }

        for (int i = 0; i < _smoothing; i++)
            SmoothMap();

        return _map;

    }

    public override int[,] SmoothMap()
    {
        int[,] newMap = _map;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                int neighbors = GetNeighborsNumber(i, j, 1, 1);

                if (neighbors > 4)
                    newMap[i,j] = 1;
                else if (neighbors < 4)
                    newMap[i,j] = 0;
            }
        }

        _map = newMap;
        return _map;
    }

    private Cell GetNextCell(Cell currentCell, int direction)
    {
        Cell nextCell = new Cell();

        switch(direction)
        {
            case 0: //left
                nextCell.x = currentCell.x - 1;
                nextCell.y = currentCell.y;
                break;
            case 1: // up
                nextCell.x = currentCell.x;
                nextCell.y = currentCell.y - 1;
                break;
            case 2: // right
                nextCell.x = currentCell.x + 1;
                nextCell.y = currentCell.y;
                break;
            case 3: // down
                nextCell.x = currentCell.x;
                nextCell.y = currentCell.y + 1;
                break;
        }

        return nextCell;
    }
}
