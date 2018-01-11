using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapGenerator {

    public struct Cell
    {
        public int x, y;

        public Cell(int xx, int yy)
        {
            x = xx;
            y = yy;
        }
    }

    protected int[,] _map;
    protected int _width;
    protected int _height;
    protected int _fillPercentage;
    protected int _smoothing;

    public MapGenerator()
    {
        _map = new int[10, 10];
        _width = 10;
        _height = 10;
        _fillPercentage = 50;
        _smoothing = 5;
    }

    public MapGenerator(int width, int height, int fillPercentage, int smoothing)
    {
        _map = new int[width, height];
        _width = width;
        _height = height;
        _fillPercentage = fillPercentage;
        _smoothing = smoothing;
    }

    public abstract int[,] GenerateMap();
    public abstract int[,] SmoothMap();
    public abstract int[,] CleanMapWalls(int wallThresholdSize);

    protected int GetNeighborsNumber(int x, int y, int stepX, int stepY)
    {
        int neighbors = 0;

        int minX = x - stepX;
        int maxX = x + stepX;
        int minY = y - stepY;
        int maxY = y + stepX;

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (!IsOutOfBounds(i, j))
                {
                    if (i != x || j != y)
                        neighbors += _map[i, j];
                }
                else
                    neighbors++;
            }
        }

        return neighbors;
    }

    protected bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || x >= _width || y < 0 || y >= _height;
    }
}
