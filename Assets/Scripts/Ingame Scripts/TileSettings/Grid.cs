using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid
{

    private const int _cellSize = 1;
    private int[,] _grid;
    

    public Grid(int width, int height, GameObject gridStorage)
    {
       
       _grid = new int[width, height];
       
       for (int x = 0; x <= _grid.GetLength(0); x++)
       {
           for (int y = 0; y <= _grid.GetLength(1); y++)
           {
               string gridName = $"Tile[x:{x}, y:{y}]";
               
               GameObject tile = new GameObject(gridName);
               tile.AddComponent<BoxCollider2D>();
               tile.layer = 1;
               
               SetTileMainComponent(tile, x, y, width, height);
               SetTilePosition(tile, gridStorage, x, y, _cellSize);
               SetTileRandomRotation(tile);
           }
       }
    }

    private void SetTileRandomRotation(GameObject tile)
    {
        int amountOfSteps = Random.Range(1, 4);
        tile.transform.Rotate(0,0,90 * amountOfSteps);
    }
    private void SetTileMainComponent(GameObject tile, int x, int y, int width, int height)
    {
        bool isEndOfMap = (x == 0) || (x == width) || (y == 0) || y == height;
        bool isSand = (x == 1) || (x == width - 1) || (y == 1) || y == height - 1;

        if (isEndOfMap)
        {
            tile.layer = 0;
        }
        else
        {
            if (isSand)
            {
                tile.AddComponent<Sand>();
            }
            else
            {
                tile.AddComponent<Grass>();
            }
        }
    }

    private void SetTilePosition(GameObject tile, GameObject storage, int x, int y, int cellSize)
    {
        tile.transform.position = new Vector3(x, y) + new Vector3(cellSize, cellSize) * .5f;
        tile.transform.parent = storage.transform;  
    }
    

    private Vector3 GetWorldCellPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }
}
