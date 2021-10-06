using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid
{
    private float _cellSize;
    public float CellSize 
    { 
        get => _cellSize;
        private set => _cellSize = value;
    }

    public int width;
    public int height;
    public GameObject[,] GridTerritories;


    /// <summary>
    /// Create Grid with given width and height, with size of cellSize
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="cellSize"></param>
    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.CellSize = cellSize;

        GridTerritories = new GameObject[width, height];
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * CellSize;
    }

    /// <summary>
    /// Spawn <see cref="spawnObject"/> at given position on Grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="spawnObject"></param>
    /// <param name="parent"></param>
    public void SetObjectPos(int x, int y, GameObject spawnObject, Transform parent)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            var objPos = GetWorldPosition(x, y) + new Vector3(CellSize, .5f, CellSize) * .5f;
            var territoryObj = GameObject.Instantiate(spawnObject, objPos, Quaternion.identity, parent);

            if (territoryObj.GetComponent<Territory>() != null)
            {
                territoryObj.GetComponent<Territory>().TerritoryQuad.transform.localScale *= CellSize;
            }

            GridTerritories[x, y] = territoryObj;
        }
        else
        {
            Debug.Log("Out of range");
        }
    }

    /// <summary>
    /// Get GameObject situated on <see cref="objectPos"/> pos
    /// Return null in case <see cref="objectPos"/>  out of grid or null object at given Position
    /// </summary>
    /// <param name="objectPos">Position on grid</param>
    /// <returns></returns>
    public GameObject GetObjectAtPos(Vector2 objectPos)
    {
        var x = (int)objectPos.x;
        var y = (int)objectPos.y;

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (GridTerritories[x, y].gameObject != null)
            {
                var obj = GridTerritories[x, y].gameObject;
                return obj;
            }
        }

        return null;
    }

    /// <summary>
    /// Return true if all map territories are type of <see cref="territoryType"/> 
    /// </summary>
    /// <param name="territoryType"></param>
    /// <returns></returns>
    public bool IsAllMapOfType(TerritoryType territoryType)
    {
        List<GameObject> territories = GridTerritories.Cast<GameObject>().ToList();

        return territories.All(f => f.GetComponent<Territory>().TerritoryType == territoryType);
    }

    /// <summary>
    /// Return if <see cref="territoryType"/> is available and exist on map
    /// </summary>
    /// <param name="territoryType"></param>
    /// <returns></returns>
    public bool IsAnyTypeTerritory(TerritoryType territoryType)
    {
        List<GameObject> territories = GridTerritories.Cast<GameObject>().ToList();

        return territories.Any(f => f.GetComponent<Territory>().TerritoryType == TerritoryType.Player);
    }
}
