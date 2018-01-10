using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkGenerator : MapGenerator
{
    public RandomWalkGenerator()
        : base()
    {
    }

    public RandomWalkGenerator(int width, int height, int fillPercentage, bool autoSmoothing, int smoothing)
        : base(width, height, fillPercentage, autoSmoothing, smoothing)
    {
    }

    public override int[,] CleanMapWalls(int wallThresholdSize)
    {
        return _map;
    }

    public override int[,] GenerateMap()
    {
        return _map;
    }

    public override int[,] SmoothMap()
    {
        return _map;
    }
}
