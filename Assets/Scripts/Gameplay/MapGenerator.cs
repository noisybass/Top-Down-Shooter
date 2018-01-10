using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapGenerator {

    struct Cell
    {
        int x, y;
    }

    protected int[,] _map;
    protected int _width;
    protected int _height;
    protected int _fillPercentage;
    protected bool _autoSmoothing;
    protected int _smoothing;

    public MapGenerator()
    {
        _map = new int[10, 10];
        _width = 10;
        _height = 10;
        _fillPercentage = 50;
        _autoSmoothing = false;
        _smoothing = 5;
    }

    public MapGenerator(int width, int height, int fillPercentage, bool autoSmoothing, int smoothing)
    {
        _map = new int[width, height];
        _width = width;
        _height = height;
        _fillPercentage = fillPercentage;
        _autoSmoothing = autoSmoothing;
        _smoothing = smoothing;
    }

    public abstract int[,] GenerateMap();
    public abstract int[,] SmoothMap();
    public abstract int[,] CleanMapWalls(int wallThresholdSize);
}
