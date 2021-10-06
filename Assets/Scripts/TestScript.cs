using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static Grid Grid;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject NeutralPrefab;

    private int _width = 3;
    private int _height = 4;

    // Start is called before the first frame update
    private void Start()
    {
        Grid = new Grid(_width, _height, 5);
        Grid.SetObjectPos(0, 0, PlayerPrefab);
        Grid.SetObjectPos(_width - 1, _height - 1, EnemyPrefab);

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                if ((y == 0 && x == 0) || (x == _width - 1 && y == _height - 1))
                    continue;

                Grid.SetObjectPos(x, y, NeutralPrefab);
            }
        }
    }
}
