using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public enum GeneratorType
    {
        RANDOM_WALK,
        CELLULAR_AUTOMATA
    }

    private MapGenerator _mapGen;
    private int[,] _map;
    public GeneratorType genType;
    public int mapWidth;
    public int mapHeight;
    private int _width;
    private int _height;
    public int fillPercentage;
    public int smoothing;
    public GameObject[] mapTiles;
    public int xSeparation = 6;
    public int ySeparation = 6;
    public int border = 6;

    private void Awake()
    {
        switch(genType)
        {
            case GeneratorType.RANDOM_WALK:
                _mapGen = new RandomWalkGenerator(mapWidth, mapHeight, fillPercentage, smoothing);
                break;
            case GeneratorType.CELLULAR_AUTOMATA:
                break;
        }

        _width = mapWidth + border;
        _height = mapHeight + border;
        _map = new int[_width, _height];

        GenerateMap();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    List<GameObject> children = new List<GameObject>();
        //    foreach (Transform child in transform) children.Add(child.gameObject);
        //    children.ForEach(child => Destroy(child));

        //    GenerateMap();
        //    InstantiateMap();
        //}
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    List<GameObject> children = new List<GameObject>();
        //    foreach (Transform child in transform) children.Add(child.gameObject);
        //    children.ForEach(child => Destroy(child));

        //    SmoothMap();
        //    InstantiateMap();
        //}
    }

    private void SmoothMap()
    {
        for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
                _map[i, j] = 1;

        int[,] innerMap = _mapGen.SmoothMap();
        for (int i = border / 2; i < mapWidth + border / 2; i++)
            for (int j = border / 2; j < mapHeight + border / 2; j++)
                _map[i, j] = innerMap[i - border / 2, j - border / 2];

        //Central zone empty
        for (int i = mapWidth / 2 - 1; i < mapWidth / 2 + 1; i++)
            for (int j = mapHeight / 2 - 1; j < mapHeight / 2 + 1; j++)
                innerMap[i, j] = 0;
    }

    public void GenerateMap()
    {
        for (int i = 0; i < _width; i++)
            for (int j = 0; j < _height; j++)
                _map[i, j] = 1;

        int[,] innerMap = _mapGen.GenerateMap();

        //Central zone empty
        for (int i = mapWidth / 2 - 2; i < mapWidth / 2 + 2; i++)
            for (int j = mapHeight / 2 - 2; j < mapHeight / 2 + 2; j++)
                innerMap[i, j] = 0;

        for (int i = border/2; i < mapWidth + border/2; i++)
            for (int j = border/2; j < mapHeight + border/2; j++)
                _map[i, j] = innerMap[i - border/2, j - border/2];

        CleanMap();
        InstantiateMap();
    }

    private void CleanMap()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    private void InstantiateMap()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_map[i, j] == 1)
                {
                    int randomIndex = UnityEngine.Random.Range(0, mapTiles.Length);
                    if (j % 2 == 0)
                        GameObject.Instantiate(mapTiles[randomIndex], new Vector3((i - _width / 2) * xSeparation, (j - _height / 2) * ySeparation, 0), Quaternion.identity, transform);
                    else
                        GameObject.Instantiate(mapTiles[randomIndex], new Vector3((i - _width / 2) * xSeparation + xSeparation / 2.0f, (j - _height / 2) * ySeparation, 0), Quaternion.identity, transform);
                }
            }
        }
    }
}
