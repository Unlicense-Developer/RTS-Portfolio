using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool isWater;
}


public class Grid : MonoBehaviour
{
    public float waterLevel = .4f;
    public int size = 100;

    Cell[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        MakeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeGrid()
    {
        grid = new Cell[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                cell.isWater = false;
                grid[x, y] = cell;
            }
        }
    }

    void MakeNoiseMap()
    {
        float[,] noiseMap = new float[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * size, y * size);
                noiseMap[x, y] = noiseValue;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];

                if (cell.isWater)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.green;

                Vector3 pos = new Vector3(x, 0, y);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}
