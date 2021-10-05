using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                var objPos = GetWorldPosition(x, y) + new Vector3(cellSize,.5f, cellSize) * .5f;
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize;
    }

    public void SetObjectPos(int x, int y, GameObject spawnObject)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            var objPos = GetWorldPosition(x, y) + new Vector3(cellSize, .5f, cellSize) * .5f;
            GameObject.Instantiate(spawnObject, objPos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Out of range");
        }
    }
}
