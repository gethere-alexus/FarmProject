using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private const int _cellSize = 1;
    private int[,] _grid;
    private GameObject _gridStorage;
    

    public Grid(int _width, int _height, GameObject _gridStorage)
    {
       this._width = _width;
       this._height = _height;
       this._gridStorage = _gridStorage;
       
       _grid = new int[_width, _height];
       
       for (int x = 0; x <= _grid.GetLength(0); x++)
       {
           for (int y = 0; y <= _grid.GetLength(1); y++)
           {
               string gridName = $"Tile[x:{x}, y:{y}]";
               
               GameObject tile = new GameObject(gridName);
               tile.AddComponent<BoxCollider2D>();
               tile.layer = 1;
               
               bool isEndOfMap = (x == 0) || (x == _width) || (y == 0) || y == _height;
               bool isSand = (x == 1) || (x == _width - 1) || (y == 1) || y == _height - 1;
               
               if (isEndOfMap)
               {
                   tile.layer = 0;
               }
               else 
               {
                   if (isSand)
                   {
                       tile.AddComponent<SandTile>();
                   }
                   else
                   {
                       tile.AddComponent<DefaultTile>();
                   }
               }
               
               tile.transform.position = new Vector3(x, y) + new Vector3(_cellSize, _cellSize) * .5f;
               tile.transform.parent = _gridStorage.transform;  
           }
       }
    }
    

    private Vector3 GetWorldCellPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }
}
