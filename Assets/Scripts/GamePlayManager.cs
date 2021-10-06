using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static Grid Grid;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject NeutralPrefab;
    public Transform TerritoriesParent;
    public Transform TerritoriesNeutralParent;

    [Header("Map dimension")]
    public int GridWidth = 3;
    public int GridHeight = 4;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1f;

        GridWidth = Random.Range(2, 5);
        GridHeight = Random.Range(2, 5);

        var playerPosX = Random.Range(0, GridWidth);
        var playerPosY = Random.Range(0, GridHeight);

        var enemyPosX = RandomIntExcept(0, GridWidth, playerPosX);
        var enemyPosY = RandomIntExcept(0, GridHeight, playerPosY);

        Grid = new Grid(GridWidth, GridHeight, 5);
        Grid.SetObjectPos(playerPosX, playerPosY, PlayerPrefab, TerritoriesParent);
        Grid.SetObjectPos(enemyPosX, enemyPosY, EnemyPrefab, TerritoriesParent);

        for (var x = 0; x < GridWidth; x++)
        {
            for (var y = 0; y < GridHeight; y++)
            {
                if ((x == playerPosX && y == playerPosY) || (x == enemyPosX && y == enemyPosY))
                    continue;

                Grid.SetObjectPos(x, y, NeutralPrefab, TerritoriesNeutralParent);
            }
        }

        var cameraPos = new Vector3((float)GridWidth / 2, 5, (float)GridHeight / 2) * Grid.CellSize;
        Camera.main.transform.position = cameraPos;
    }

    private int RandomIntExcept(int min, int max, int except)
    {
        int result = Random.Range(min, max - 1);
        if (result >= except) result += 1;
        return result;
    }


}
