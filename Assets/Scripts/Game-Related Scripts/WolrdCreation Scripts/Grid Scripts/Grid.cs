using UnityEngine;
using Random = UnityEngine.Random;

public class Grid
{
    
    private const int CellSize = 1;
    private int[,] _grid;

    private GameObject _borderTile;
    private GameObject _sandTile;
    private GameObject _grassTile;
    private GameObject _dirtTile;
    private GameObject _cultivatedDirt;
    

    public Grid(int width, int height, GameObject gridStorage, GameObject border, GameObject sand, GameObject grass, GameObject dirt, GameObject cultivatedDirt)
    {
        
        _borderTile = border;
        _sandTile = sand;
        _grassTile = grass;
        _dirtTile = dirt;
        _cultivatedDirt = cultivatedDirt;
        
       _grid = new int[width, height];
       
       for (int x = 0; x <= _grid.GetLength(0); x++)
       {
           for (int y = 0; y <= _grid.GetLength(1); y++)
           {
               string gridName = $"Tile[x:{x}, y:{y}]";
               GameObject tile = InstantiateTile(x, y, width, height);
               SetTilePosition(tile, gridStorage, x, y, CellSize);
               SetTileRandomRotation(tile);
           }
       }
    }

    private void SetTileRandomRotation(GameObject tile)
    {
        int amountOfSteps = Random.Range(1, 4);
        tile.transform.Rotate(0,0,90 * amountOfSteps);
    }
    private GameObject InstantiateTile(int x, int y, int width, int height)
    {
        GameObject tile;
        bool isEndOfMap = (x == 0) || (x == width) || (y == 0) || y == height;
        // removed from game - bool isSand = (x == 1) || (x == width - 1) || (y == 1) || y == height - 1;

        if (isEndOfMap)
        {
            tile = Object.Instantiate(_borderTile);
        }
        else
        {
            tile = Object.Instantiate(_grassTile);
        }
        return tile;
    }

    private void SetTilePosition(GameObject tile, GameObject storage, int x, int y, int cellSize)
    {
        tile.transform.position = new Vector3(x, y) + new Vector3(cellSize, cellSize) * .5f;
        tile.transform.parent = storage.transform;  
    }
    

    private Vector3 GetWorldCellPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize;
    }
}
