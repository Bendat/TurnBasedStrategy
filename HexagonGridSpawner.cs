using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGridSpawner : MonoBehaviour {

    public int Width;
    public int Height;
    public Transform Tile;

    public Transform[,] Grid;

    float lgt;
    float hgt;
    float oddRowOffset;

    void Start()
    {
        SpawnGrid();
        PrintGridToConsole();
    } 

    /// <summary>
    /// Gets the width and length of Transform Tile
    /// </summary>
    void GetWidthAndLengthOfTile()
    {
        lgt = Tile.GetComponent<Renderer>().bounds.size.x;
        hgt = Tile.GetComponent<Renderer>().bounds.size.z;
        oddRowOffset = lgt / 2;
    }

    /// <summary>
    /// Creates a hex grid
    /// The Grid matrix will contain the transform of each tile
    /// </summary>
    void SpawnGrid()
    {
        GetWidthAndLengthOfTile();

        Grid = new Transform[Width, Height];
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Transform tile = Instantiate(Tile, transform);
                Grid[x, y] = tile;
                if (y % 2 == 0)
                {
                    tile.position = new Vector3(x * lgt, 0, y * hgt * 0.75f);
                }
                else
                {
                    tile.position = new Vector3((x * lgt) + oddRowOffset, 0, y * hgt * 0.75f);
                }
                tile.name = x + ", " + y;
            }
        }
    }

    /// <summary>
    /// Prints out the grid in console
    /// </summary>
    void PrintGridToConsole()
    {
        for(int x = 0; x < Grid.GetLength(0); x++)
        {
            for(int y = 0; y < Grid.GetLength(1); y++)
            {
                Debug.Log(x + "," + y + ": " + Grid[x,y].name);
            }
        }
    }

}
