using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralTerritory : BotTerritory
{
    [Header("Range for MaxPoints")]
    public int MaxPointsMin;
    public int MaxPointsMax;

    private int _currentMaxPoint;

    private void Awake()
    {
        _currentMaxPoint = Random.Range(MaxPointsMin, MaxPointsMax);
    }

    public override void UpdatePoints()
    {
        base.UpdatePoints();

        if (CurrentPoints > _currentMaxPoint)
        {
            CurrentPoints = _currentMaxPoint;
        }
    }

    public override void SetTextMeshPro()
    {
        PointsTxtPro.text = $"{CurrentPoints}/{_currentMaxPoint}";
    }
}
